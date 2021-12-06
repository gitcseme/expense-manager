<template>
  <div class="expense-wrapper">
    <vue-confirm-dialog></vue-confirm-dialog>
    <keep-alive>
        <ExpenseModal 
            @close="hideDialog" 
            @onCreateExpense="onCreateExpense"
            @onUpdateExpense="onUpdateExpense"
            :options="vmCategoryTree"
            :expense="vmExpense"
            :mode="vmMode"
            v-if="dialogIsVisible"
        >
            <h3 slot="heading">Create Expense</h3>
            <button class="btn btn-warning mr-1" @click="hideDialog">Close</button>
        </ExpenseModal>
    </keep-alive>
    <div class="row">
      <div class="col-12">
        <button class="btn btn-primary" @click="showDialog('create')">Create single expense</button>
        <router-link class="btn btn-outline-primary" :to="{ name: 'bulk-expense' }">Bulk expense entry</router-link>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-md-3">
        <div class="data-wrapper card p-3" :style="{ backgroundColor: 'coral' }">
          <h4>Total Expense</h4>
          <h5>{{ vmTotalExpense }}</h5>
        </div>
      </div>
    </div>

    <div class="row">
        <div class="col-6">
            <h3>Expense history</h3>
        </div>
        <div class="col-6">
            <Treeselect class="float-right" :style="{ width: '50%'}" v-model="vmCategoryId" :options="vmCategoryTree"></Treeselect>
        </div>
    </div>

    <div class="row expense-table-outer">
        <div class="col-12 expense-table-col">
            <div class="table-wrapper table-responsive">
                <table class="table table-striped table-bordered" id="expense-history">
                    <thead>
                        <tr>
                            <!-- <th class="first-fixed-col" scope="col">Id</th> -->
                            <!-- <th scope="col">Title</th> -->
                            <th scope="col">Serial</th>
                            <th scope="col">Category</th>
                            <th scope="col">Amount</th>
                            <th scope="col">Author</th>
                            <th scope="col">Date</th>
                            <th scope="col">Paid Through</th>
                            <th scope="col">Payment Reference</th>
                            <th scope="col">Expense Reference</th>
                            <th scope="col">Description</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(expense, index) in vmExpenses" :key="expense.id">
                            <!-- <td class="first-fixed-col" scope="row">
                                <div class="data-expense-uniqueId">
                                    {{ expense.uniqueCode }}
                                </div>
                            </td> -->
                            <!-- <td>
                                <div class="data-expense">
                                    {{ expense.title }}
                                </div>
                            </td> -->
                            <td>
                               {{index+1}}
                            </td>
                            <td>
                                <div class="data-expense-category">
                                    {{ expense.CategoryLabel }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense-amount">
                                    {{ expense.Amount }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense">
                                    {{ expense.Author }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense-date">
                                    {{ expense.Time }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense-payOption">
                                    {{ expense.PaymentMode }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense">
                                    {{ expense.PaymentReference }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense">
                                    {{ expense.ExpenseReferenceId }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense">
                                    {{ expense.Description }}
                                </div>
                            </td>
                            <td>
                                <div class="data-expense-actions">
                                    <button @click="onEditExpense(expense.Id)" class="btn btn-warning btn-sm mr-1">
                                        <i class="ti-marker-alt"></i>
                                    </button>
                                    <button @click="onDeleteExpense(expense.Id)" class="btn btn-danger btn-sm">
                                        <i class="ti-trash"></i>
                                    </button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <Pagination 
        label="expenses" 
        :value="vmPaginationModel"
        @pageChanged="onPageChanged"
        @pageSizeChanged="onPageSizeChanged"
    ></Pagination>
  </div>
</template>

<script>
import Card from "@scripts/components/Cards/Card.vue";
import ExpenseModal from "@scripts/components/ExpenseModal.vue";
import Expense from '@scripts/data/Expense';
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import Utility from "@scripts/Utilities/CommonUtilityFunction";
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import ExpenseService from "@scripts/Services/ExpenseService";
import CategoryService from "@scripts/Services/CategoryService";
import Pagination from "@scripts/components/Pagination.vue";
import PaginationModel from "@scripts/Utilities/Pagination"; 

export default {
  components: {
    Card,
    ExpenseModal,
    Treeselect,
    Pagination
  },
  data() {
    return {
      vmMode: 'create',
      vmExpense: new Expense(),
      vmExpenses: [],
      searchText: '',
      dialogIsVisible: false,
      vmCategoryTree: [],
      vmCategoryId: 0,
      vmTotalExpense: 0,
      companyId: null,
      vmPaginationModel: new PaginationModel(),
    };
  },
  mounted() {
    this.companyId = ApplicationContextService.getContext().company.id;
    this.loadCategoryTree();
    this.loadExpenses();
  },
  methods: {
    loadExpenses() {
        let categoryId = this.vmCategoryId == null ? 0 : this.vmCategoryId;
        Utility.prepareCategories(this.vmCategoryTree, [categoryId]).then(catIdList => {
            let catIds = catIdList.join(',');

            ExpenseService.getAllExpense(this.companyId, categoryId, catIds, this.vmPaginationModel)
            .then(response => {
                console.log('new-res: ', response);
                let totalExpense = 0;
                response.items.forEach(e => {
                    totalExpense += e.Amount;
                    e.Amount = Utility.formatMoney(e.Amount);
                });
                this.vmExpenses = response.items;
				this.vmPaginationModel = response.pagination;
                this.vmTotalExpense = Utility.formatMoney(totalExpense);
            });
        });

    },
    loadCategoryTree() {
      CategoryService.getAllCategories(this.companyId)
      .then(data => {
        this.vmCategoryTree = JsonTreeBuilder.buildTree(data);
        this.vmCategoryTree.unshift({ id: 0, label: 'All', children: [] });
      });
    },
    onSearchExpenses() {
      console.log('search-text: ', this.searchText);
    },
    onCreateExpense(expense) {
      expense.companyId = this.companyId;
      ExpenseService.createExpense(expense)
        .then(() => {
          this.loadExpenses();
          Utility.showNotification(this, 'New expense is created', 'ti-announcement', 'success');
        });
    },
    onUpdateExpense(updatedExpense) {
      ExpenseService.updateExpense(updatedExpense)
        .then(() => {
          this.loadExpenses();
          Utility.showNotification(this, 'Expense is updated', 'ti-announcement', 'warning');
        });
    },
    onEditExpense(id) {
      let exp = this.vmExpenses.find(x => x.id === id);
      exp.amount = Utility.formattedToNormalMoney(exp.amount);
      this.vmExpense = new Expense(exp);
      this.showDialog('update');
    },
    onDeleteExpense(id) {
      this.$confirm({
        title: 'Are you sure?',
        message: `Press Yes to delete or No to cancel`,
        button: {
          no: 'No',
          yes: 'Yes'
        },
        callback: confirm => {
          if (confirm) {
            ExpenseService.deleteExpense(id)
              .then(() => {
                this.loadExpenses();
                Utility.showNotification(this, 'Expense is deleted', 'ti-announcement', 'danger');
              });
          }
          else console.log('no');
        }
      });
    },
    showDialog(mode) {
      this.vmMode = mode;
      this.dialogIsVisible = true;
    },
    hideDialog() {
      this.dialogIsVisible = false;
      this.vmExpense = new Expense();
    },
    onPageChanged(pageIndex) {
        //this.vmPagination.pageIndex = pageIndex;
        this.loadExpenses();
        console.log('page-changed: ', this.vmPaginationModel);
    },
    onPageSizeChanged(pageSize) {
        //this.vmPagination.pageSize = pageSize;
        this.loadExpenses();
        console.log('page-size-changed: ', this.vmPaginationModel);
    },
  },
  watch: {
    vmCategoryId() {
      this.loadExpenses();
    }
  }
}
</script>
