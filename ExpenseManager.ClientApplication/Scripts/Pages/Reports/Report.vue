<template>
    <div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-label">
                    <label>Select date range</label>
                </div>
                <div class="form-group">
                    <DatePicker
                        v-validate="'required'"
                        data-vv-name="date"
                        data-vv-as="date"
                        v-model="dateRange"
                        valueType="format"
                        range 
                        placeholder="Select date range"
                    ></DatePicker>
                    <span class="text-danger">{{ errors.first('date') }}</span>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-label">
                    <label>Select categories</label>
                </div>
                <div class="form-group">
                    <Treeselect 
                        :multiple="true"
                        v-model="selectedCategories" 
                        :options="categoryTree"
                    ></Treeselect>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <button class="btn btn-outline-warning" @click="loadReport">Filter report</button>
                <button class="btn btn-outline-success" @click="downloadReport">Download Report</button>
            </div>
        </div>
        <div class="expense-wrapper">
            <div class="row">
                <div class="col-12">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col" class="font-weight-bold">UID</th>
                                <th scope="col" class="font-weight-bold">Title</th>
                                <th scope="col" class="font-weight-bold">Category</th>
                                <th scope="col" class="font-weight-bold">Amount</th>
                                <th scope="col" class="font-weight-bold">Author</th>
                                <th scope="col" class="font-weight-bold">Date</th>
                                <th scope="col" class="font-weight-bold">Payment Mode</th>
                                <th scope="col" class="font-weight-bold">Payment Reference</th>
                                <th scope="col" class="font-weight-bold">Expense Reference</th>
                                <th scope="col" class="font-weight-bold">Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="expense in reportData" :key="expense.id">
                                <th scope="row">{{ expense.uniqueCode }}</th>
                                <td>{{ expense.title }}</td>
                                <td>{{ expense.categoryLabel }}</td>
                                <td>{{ expense.amount }}</td>
                                <td>{{ expense.author }}</td>
                                <td>{{ expense.time }}</td>
                                <td>{{ expense.paymentMode }}</td>
                                <td>{{ expense.paymentReference }}</td>
                                <td>{{ expense.expenseReferenceId }}</td>
                                <td>{{ expense.description }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import CategoryService from "@scripts/Services/CategoryService";
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import ReportService from "@scripts/Services/ReportService";
import Utility from "@scripts/Utilities/CommonUtilityFunction";

export default {
    components: {
        DatePicker,
        Treeselect
    },
    data() {
        return {
            dateRange: ['2021-01-01', new Date().toISOString().split('T')[0]],
            selectedCategories: [],
            categoryTree: [],
            companyId: null,
            reportData: [],
        };
    },
    mounted() {
        this.companyId = ApplicationContextService.getContext().company.id;
        this.loadCategoryTree();
    },
    methods: {
        loadCategoryTree() {
            CategoryService.getAllCategories(this.companyId)
            .then(data => {
                this.categoryTree = JsonTreeBuilder.buildTree(data);
                // this.categoryTree.unshift({ id: 0, label: 'All', children: [] });
                this.loadReport();
            });
        },
        async loadReport() {
            let parentAndSubCategories = await this.prepareCategoryIds();
            console.log('ids: ', parentAndSubCategories);
            ReportService.getExpenseReport(this.dateRange, parentAndSubCategories)
                .then(response => {
                    this.reportData = response.data;
                })
                .catch(error => {
                    console.log('error: ', error);
                });
        },
        async prepareCategoryIds() {
            let catIds = await Utility.prepareCategories(this.categoryTree, this.selectedCategories);
            return catIds;
        },
        async downloadReport() {
            let parentAndSubCategories = await this.prepareCategoryIds();
            ReportService.generateExpenseReport(this.dateRange, parentAndSubCategories)
                .then(() => {
                    console.log('download success');
                    ReportService.downloadReport()
                    .then(response => {
                        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
                        var fileLink = document.createElement('a');

                        fileLink.href = fileURL;
                        fileLink.setAttribute('download', 'expense-report.xlsx');
                        document.body.appendChild(fileLink);

                        fileLink.click();
                    })
                    .catch(error => {
                        console.log('dw-error: ', error);
                    });
                })
                .catch(error => {
                    console.log('download-error: ', error);
                });
        },
    }
}
</script>

<style>

</style>