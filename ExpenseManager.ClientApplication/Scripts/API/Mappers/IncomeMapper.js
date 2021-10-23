import Utility from "@scripts/Utilities/CommonUtilityFunction";

export default {
    formatIncome(data) {
        return Utility.formatMoney(data);
    },
    mapIncome(data) {
        data.forEach(i => {
            i.amount = Utility.formatMoney(i.amount);
        });
        return data;
    }
};