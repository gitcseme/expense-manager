import PaginationModel from "@scripts/Utilities/Pagination"; 

export default class List {
    items = [];
    pagination = new PaginationModel();

    constructor(value) {
        Object.assign(this, value);
    }
}