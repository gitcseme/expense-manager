import ReportAPI from "@scripts/API/ReportAPI";

export default {
    getExpenseReport(dateRange, selectedCategories) {
        return ReportAPI.getExpenseReport(dateRange, selectedCategories);
    },
    generateExpenseReport(dateRange, selectedCategories) {
        return ReportAPI.generateExpenseReport(dateRange, selectedCategories);
    },
    downloadReport() {
        return ReportAPI.downloadReport();
    }
}