<template>
    <div class="container register-wrapper">
      <div class="row">
        <div class="col-md-6 left-side">
          <div class="enkaizen-bg">
            <img src="@scripts/assets/images/enkaizen.png" alt="" srcset="">
          </div>
        </div>
        <div class="col-md-6 right-side">
          <ServerError 
              v-if="serverError"
              :message="serverError">
          </ServerError>
          <div class="card registration-form">
            <form @submit.prevent="onSubmit">
              <h3>Registration Form</h3>
              <div class="form-group">
                  <label>Full Name</label>
                  <input type="text" class="form-control" placeholder="Full name..." v-model="vmRegisterModel.fullName">
              </div>
              <div class="form-group">
                  <label>Your Company Name</label>
                  <input type="text" class="form-control" placeholder="Company name..." v-model="vmRegisterModel.companyName">
              </div>
              <div class="form-group">
                  <label>Phone Number</label>
                  <input type="text" class="form-control" placeholder="Phone number..." v-model="vmRegisterModel.phoneNumber">
              </div>
              <div class="form-group">
                  <label>Email</label>
                  <input type="text" class="form-control" placeholder="Email..." v-model="vmRegisterModel.email">
                  <span class="text-danger" v-if="emailAlreadyUsed">The email is already in use, Please use another!</span>
              </div>
              <div class="form-group">
                  <label>Password</label>
                  <input type="password" class="form-control" placeholder="Password" v-model="vmRegisterModel.password">
              </div>
              <div>
                <button class="btn btn-primary" type="submit">Register</button>
                <span class="ml-1">Already have account? try </span>
                <router-link :to="{ name: 'login' }" :style="{ textDecoration: 'underline' }">Login</router-link>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import RegisterModel from "@scripts/data/RegisterModel";
import AccountAPI from "@scripts/API/AccountAPI";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";

export default {
    data() {
        return {
            vmRegisterModel: new RegisterModel(),
            emailAlreadyUsed: false,
            serverError:""
        };
    },
    methods: {
        onSubmit() {
            console.log(this.vmRegisterModel);
            AccountAPI.emailExists(this.vmRegisterModel.email)
                .then(response => {
                if (response == false) {
                    AccountAPI.register(this.vmRegisterModel).then(() => {
                        ApplicationContextService.setup().then(() => {
                            this.$router.push("/");
                        });
                    })
                        .catch((error) => {
                        this.serverError = error;
                    });
                }
                else {
                    this.emailAlreadyUsed = response;
                }
            });
        },
        Logout() {
            AccountAPI.logout()
                .then(() => {
                ApplicationContextService.setAuthStatus(false);
                let localtion = window.location.origin + "#/login";
                window.location = localtion;
            })
                .catch(error => {
                console.log("logout: ", error);
            });
        }
    },
}
</script>
