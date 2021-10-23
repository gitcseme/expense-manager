import CategoryAPI from "@scripts/API/CategoryAPI";

export default {
    getAllCategories(companyId) {
        return CategoryAPI.getAllCategories(companyId);
    },
    createCategory(model) {
        return CategoryAPI.createCategory(model);
    }
}