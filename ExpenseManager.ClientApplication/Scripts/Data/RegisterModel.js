export default class RegisterModel {
  constructor(fullName='', companyName='', phoneNumber='', email='', password='') {
    this.fullName = fullName;
    this.companyName = companyName;
    this.phoneNumber = phoneNumber;
    this.email = email;
    this.password = password;
  }
}