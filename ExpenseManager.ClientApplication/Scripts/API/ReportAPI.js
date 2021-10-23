import Axios from "axios";

const REPORT_API_ROOT = SITE_API_ROOT + "api/report";

export default {
    getExpenseReport(dateRange, selectedCategories) {
        let reportFilter = {
            startDate: dateRange.length > 0 ? dateRange[0] : new Date(),
            endDate: dateRange.length > 0 ? dateRange[1] : new Date(),
            categories: selectedCategories.length > 0 ? selectedCategories : []
        };

        return new Promise((resolve, reject) => {
            Axios.post(`${REPORT_API_ROOT}/expense-report`, reportFilter)
            .then(response => resolve(response))
            .catch(error => reject(error));
        });
    },
    generateExpenseReport(dateRange, selectedCategories) {
        let reportFilter = {
            startDate: dateRange.length > 0 ? dateRange[0] : new Date(),
            endDate: dateRange.length > 0 ? dateRange[1] : new Date(),
            categories: selectedCategories.length > 0 ? selectedCategories : []
        };

        return new Promise((resolve, reject) => {
            Axios.post(`${REPORT_API_ROOT}/download-report`, reportFilter)
            .then(response => resolve(response))
            .catch(error => reject(error));
        });
    },
    downloadReport() {
        return new Promise((resolve, reject) => {
            Axios({ url: `${REPORT_API_ROOT}/get-file`, method: 'GET', responseType: 'blob'})
            .then(response => resolve(response))
            .catch(error => reject(error));
        })
    }
}