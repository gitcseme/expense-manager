export default class LoginModel {
  constructor(email='', password='', rememberMe=false) {
    this.email = email;
    this.password = password;
    this.rememberMe = rememberMe;
  }
}