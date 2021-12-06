import Axios from 'axios';
import IncomeMapper from '@scripts/API/Mappers/IncomeMapper';

const INCOME_API_ROOT = SITE_API_ROOT + "api/income";

export default {
  getAllIncome(companyId, paging) {
    return new Promise((resolve, reject) => {
      Axios.get(`${INCOME_API_ROOT}/getall?companyId=${companyId}&pageIndex=${paging.pageIndex}&pageSize=${paging.pageSize}`)
        .then(response => resolve(IncomeMapper.mapToClient(response.data)))
        .catch(error => reject(error));
    });
  },
  addIncome(model) {
    return new Promise((resolve, reject) => {
      Axios.post(`${INCOME_API_ROOT}/create`, model)
        .then(response => resolve(response.data))
        .catch(error => reject(error));
    });
  },
  updateIncome(model) {
    return new Promise((resolve, reject) => {
      Axios.put(`${INCOME_API_ROOT}/update?id=${model.id}`, model)
        .then(response => resolve(response.data))
        .catch(error => reject(error));
    });
  },
  deleteIncome(id) {
    return new Promise((resolve, reject) => {
      Axios.delete(`${INCOME_API_ROOT}/delete?id=${id}`)
        .then(response => resolve(response.data))
        .catch(error => reject(error));
    }); 
  },
  getBalance(companyId) {
    return new Promise((resolve, reject) => {
      Axios.get(`${INCOME_API_ROOT}/balance?companyId=${companyId}`)
        .then(response => resolve(IncomeMapper.formatIncome(response.data)))
        .catch(error => reject(error));
    });
  }
};