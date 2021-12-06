import ExpenseAPI from "@scripts/API/ExpenseAPI";

export default {
    getAllExpense(companyId, categoryId, catIds, paging) {
        return ExpenseAPI.getAllExpense(companyId, categoryId, catIds, paging);
    },
    createExpense(model) {
        return ExpenseAPI.createExpense(model);
    },
    updateExpense(model) {
        return ExpenseAPI.updateExpense(model);
    },
    deleteExpense(id) {
        return ExpenseAPI.deleteExpense(id);
    },
    bulkInsert(expenses) {
        return ExpenseAPI.bulkInsert(expenses);
    }
}