import Axios from 'axios';

const CATEGORY_API_ROOT = SITE_API_ROOT + "api/category";

export default {
  getAllCategories(companyId) {
    return new Promise((resolve, reject) => {
      Axios.get(CATEGORY_API_ROOT + `/getall?companyId=${companyId}`)
        .then(response => resolve(response.data))
        .catch(error => reject(error));
    });
  },
  createCategory(model) {
    return new Promise((resolve, reject) => {
      Axios.post(CATEGORY_API_ROOT + "/create", model)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  }
};