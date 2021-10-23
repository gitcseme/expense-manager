import Axios from "axios";
import LocalStorageService from "../services/LocalStorageService";

const ACCOUNT_API_ROOT = SITE_API_ROOT + "api/account";

export default {
  getApplicationContext() {
    return new Promise((resolve, reject) => {
      Axios.get(ACCOUNT_API_ROOT + "/getApplicationContext")
        .then(response => resolve(response))
        .catch(error => {
          LocalStorageService.clearAuthenticationToken();
          reject(error);
        });
    });
  },
  register(registerModel) {
    return new Promise((resolve, reject) => {
      Axios.post(ACCOUNT_API_ROOT + "/register", registerModel)
        .then(response => {
          LocalStorageService.setAuthenticationToken(response.data.apiToken);
          resolve();
        })
        .catch(error => reject(error));
    });
  },
  login(loginModel) {
    return new Promise((resolve, reject) => {
      Axios.post(ACCOUNT_API_ROOT + "/login", loginModel)
        .then(response => {
          console.log("login-response: ", response.data);
          LocalStorageService.setAuthenticationToken(response.data.apiToken);
          resolve();
        })
        .catch(error => reject(error));
    });
  },
  logout() {
    return new Promise((resolve, reject) => {
      Axios.post(ACCOUNT_API_ROOT + "/logout")
        .then(() => {
          LocalStorageService.clearAuthenticationToken();
          resolve();
        })
        .catch(error => reject(error));
    });
  },
  emailExists(email) {
    return new Promise((resolve, reject) => {
      Axios.post(ACCOUNT_API_ROOT + `/emailExists?email=${email}`)
        .then(response => resolve(response.data))
        .catch(error => reject(error));
    });
  }
};