import ExpenseAPI from "@scripts/API/ExpenseAPI";

export default {
    getAllExpense(companyId, categoryId, paging) {
        return ExpenseAPI.getAllExpense(companyId, categoryId, paging);
    },
    createExpense(model) {
        return ExpenseAPI.createExpense(model);
    },
    updateExpense(model) {
        return ExpenseAPI.updateExpense(model);
    },
    deleteExpense(id) {
        return ExpenseAPI.deleteExpense(id);
    }
}