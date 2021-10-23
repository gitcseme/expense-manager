<template>
  <div class="container register-wrapper">
      <div class="row">
        <div class="col-md-6 left-side">
          <div class="enkaizen-bg">
            <img src="@scripts/assets/images/enkaizen.png" alt="" srcset="">
          </div>
        </div>
        <div class="col-md-6 right-side">
          <div class="card form-wrapper">
            <form @submit.prevent="onLogin">
              <h3>Login Form</h3>
              <div class="form-group">
                  <label>Email</label>
                  <input type="text" class="form-control" placeholder="Email..." v-model="vmLoginModel.email">
              </div>
              <div class="form-group">
                  <label>Password</label>
                  <input type="text" class="form-control" placeholder="Password" v-model="vmLoginModel.password">
              </div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" v-model="vmLoginModel.rememberMe">
                <label class="form-check-label">Remember Me</label>
              </div>
              <button class="btn btn-primary" type="submit">Login</button> <br/>
              <span>Don't have an account ?</span>
              <router-link :to="{ name: 'register' }" :style="{ textDecoration: 'underline' }"> Register</router-link>
            </form>
          </div>
        </div>
      </div>
  </div>
</template>

<script>
import LoginModel from "@scripts/data/LoginModel";
import AccountAPI from "@scripts/API/AccountAPI";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";

export default {
  data() {
    return {
      vmLoginModel: new LoginModel()
    };
  },
  methods: {
    onLogin() {
      AccountAPI.login(this.vmLoginModel).then(() => {
        ApplicationContextService.setup().then(() => {
          this.$router.push("/");
        });
      });
    }
  }
}
</script>

<style>
  /* body {
    background-color: cornflowerblue !important;
  }
  .enkaizen-bg {
    text-align: center;
  }
  .enkaizen-bg img {
    height: 100%;
    width: 100%;
  }
  .form-wrapper {
    padding: 10px;
  }
  .card.form-wrapper {
    height: 100%;
  } */
</style>