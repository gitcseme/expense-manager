import Axios from 'axios';
import ExpenseMapper from '@scripts/API/Mappers/ExpenseMapper';

const EXPENSE_API_ROOT = SITE_API_ROOT + "api/expense";

export default {
  createExpense(model) {
    return new Promise((resolve, reject) => {
      Axios.post(EXPENSE_API_ROOT + "/create", model)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  },
  getAllExpense(companyId, categoryId, catIds, paging) {
    return new Promise((resolve, reject) => {
      Axios.get(`${EXPENSE_API_ROOT}/getall/${categoryId}?companyId=${companyId}&catIds=${catIds}&pageIndex=${paging.pageIndex}&pageSize=${paging.pageSize}`)
        .then(response => resolve(ExpenseMapper.mapToClient(response.data)))
        .catch(error => reject(error));
    });
  },
  updateExpense(updateModel) {
    return new Promise((resolve, reject) => {
      Axios.put(EXPENSE_API_ROOT + `/update?id=${updateModel.id}`, updateModel)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  },
  deleteExpense(id) {
    return new Promise((resolve, reject) => {
      Axios.delete(`${EXPENSE_API_ROOT}/delete?id=${id}`)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  },
  bulkInsert(expenses) {
    return new Promise((resolve, reject) => {
      Axios.post(EXPENSE_API_ROOT + "/bulk-insert", expenses)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  }
}