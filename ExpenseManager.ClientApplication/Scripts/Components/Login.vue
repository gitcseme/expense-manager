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
              <div class="form-group"><label>Email</label><input type="text" class="form-control" 
				  placeholder="Email..." v-model="vmLoginModel.email"
				  data-vv-name="email" data-vv-as="Email" v-validate="'required|email'">
              </div>
              <div class="form-group">
                  <label>Password</label>
                  <input type="text" class="form-control" placeholder="Password" v-model="vmLoginModel.password">
              </div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" v-model="vmLoginModel.rememberMe">
                <label class="form-check-label">Remember Me</label>
              </div>
              <!-- <button class="btn btn-primary" type="submit">Login</button> <br/> -->
              <LoadingButton  
                  classList="btn btn-primary" 
                  text="Login" type="submit" 
                  :show_loading="loading">
              </LoadingButton>
              <span>Don't have an account ?</span>
              <router-link :to="{ name: 'register' }" :style="{ textDecoration: 'underline' }"> Register</router-link>
            </form>
			<ServerError 
				v-if="serverError" 
				:message="serverError">
			</ServerError>
          </div>
        </div>
      </div>
  </div>
</template>

<script>
import LoginModel from "@scripts/data/LoginModel";
import AccountAPI from "@scripts/API/AccountAPI";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import LoadingButton from "./LoadingButton.vue";

export default {
  components:{
    LoadingButton,
  },
  data() {
    return {
      vmLoginModel: new LoginModel(),
      loading: false, 
	  serverError: ""
    };
  },
  methods: {
    onLogin() {
	  this.$validator.validateAll().then((result) => {
		if(result) {
			this.loading = true;
      		AccountAPI.login(this.vmLoginModel)
      		.then((response) => {
				this.loading = false;
				ApplicationContextService.setup().then(() => {
					this.$router.push("/");
				});
      		})
	  		.catch((err) => {
				this.loading = false;
				this.serverError = err;
	  		});
		}
	  })	
    }
  }
}
</script>

