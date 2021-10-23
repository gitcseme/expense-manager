import IncomeAPI from "@scripts/API/IncomeAPI";

export default {
    getBalance(companyId) {
        return IncomeAPI.getBalance(companyId);
    },
    getAllIncome(companyId, paging) {
        return IncomeAPI.getAllIncome(companyId, paging);
    },
    addIncome(model) {
        return IncomeAPI.addIncome(model);
    },
    updateIncome(model) {
        return IncomeAPI.updateIncome(model);
    },
    deleteIncome(id) {
        return IncomeAPI.deleteIncome(id);
    }
}