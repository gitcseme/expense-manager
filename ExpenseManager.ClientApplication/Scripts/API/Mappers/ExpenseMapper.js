import List from "@scripts/Utilities/List";
import PaginationModel from "@scripts/Utilities/Pagination";

export default { 
    mapToClient(data) { 
        if (!!data) {
            return new List({
                items: data.Items,
                pagination: new PaginationModel({
                    pageSize: data.PageSize,
                    pageIndex: data.PageIndex,
                    itemsTotal: data.TotalItems,
                })
            });
        }
        else {
            return new List({
                items: [],
                pagination: new PaginationModel()
            });
        }
    }
}