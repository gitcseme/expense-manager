import Utility from "@scripts/Utilities/CommonUtilityFunction";
import List from "@scripts/Utilities/List";
import PaginationModel from "@scripts/Utilities/Pagination";

export default {
    formatIncome(data) {
        return Utility.formatMoney(data);
    },
    mapIncome(items) {
        items.forEach(i => {
            i.Amount = Utility.formatMoney(i.Amount);
        });
        return items;
    },
    mapToClient(data) {
        if (!!data) {
            return new List({
                items: this.mapIncome(data.Items),
                pagination: new PaginationModel({
                    pageSize: data.PageSize,
                    pageIndex: data.PageIndex,
                    itemsTotal: data.TotalItems,
                })
            });
        }
    }
};