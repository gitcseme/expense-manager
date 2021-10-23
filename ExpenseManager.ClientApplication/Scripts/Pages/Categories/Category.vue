<template>
  <div class="category-wrapper">
    <div class="row">
      <div class="col-12 mb-3">
        <button 
          :class="{ 'btn btn-lg': true, 'btn-success': vmPresentationSwitch, 'btn-primary': !vmPresentationSwitch }"
          @click="toggleView">
          Switch to {{ categoryView }}
        </button>
      </div>
    </div>
		<div class="row">
			<div v-if="vmPresentationSwitch" class="col-12">
				<CategoryTreeView 
          v-if="dataLoaded"
					:tree="vmCategoryTree" 
					:marginLeft="marginLeft"
				></CategoryTreeView>
			</div>
      <div v-else class="col-6">
        <table class="table table-bordered table-striped">
          <thead>
            <tr>
              <th scope="col">Category</th>
              <th scope="col">Expense</th>
            </tr>
          </thead>
          <tbody>
            <tr 
              v-for="categoryExpense in vmCategoryExpenses" :key="categoryExpense.id"
              :class="{ 'table-success': categoryExpense.expense > 0 }">
              <td scope="row">{{ categoryExpense.label }}</td>
              <td>{{ categoryExpense.expense }}</td>
            </tr>
            <tr class="table-danger">
              <th scope="row">Total Expense</th>
              <th>{{ vmTotalExpenseForCategory }}</th>
            </tr>
          </tbody>
        </table>
      </div>
		</div>
  </div>
</template>

<script>
import CategoryTreeView from '@scripts/components/TreeView.vue';
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import CategoryAPI from "@scripts/API/CategoryAPI";
import ApplicationContextService from '@scripts/Services/ApplicationContextService';

export default {
	components: {
		CategoryTreeView
	},
  data() {
    return {
      id: this.$route.params.id,
			marginLeft: 70,
      vmCategoryTree: [],
      dataLoaded: false,
      vmCategoryExpenses: [],
      vmTotalExpenseForCategory: 0,
      vmPresentationSwitch: true,
    };
  },
  mounted() {
    this.loadCategories();
  },
  computed: {
    categoryView() {
      return this.vmPresentationSwitch ? 'Tabular View' : 'Tree View';
    }
  },
  methods: {
    toggleView() {
      this.vmPresentationSwitch = !this.vmPresentationSwitch;
    },
    loadCategories() {
      let copanyId = ApplicationContextService.getContext().company.id;
      CategoryAPI.getAllCategories(copanyId)
        .then(response => {
          console.log('category(s): ', response);
          this.vmCategoryTree = JsonTreeBuilder.buildTree(response, +this.id);
          this.dataLoaded = true;
          console.log('built-tree: ', this.vmCategoryTree);

          this.vmCategoryExpenses = [];
          this.vmTotalExpenseForCategory = this.calculateExpenses(this.vmCategoryTree);
        });
    },
    calculateExpenses(tree) {
      let totalExpense = 0
      tree.forEach(node => {
        let childExpense = 0;
        node.expenses.forEach(expense => {
          childExpense += expense.amount;
        });

        this.vmCategoryExpenses.push({
          id: node.id,
          label: node.label,
          expense: childExpense
        });
        totalExpense += childExpense + this.calculateExpenses(node.children);
      });

      return totalExpense;
    }
  },
  watch: {
    $route(to, from) {
      this.id = to.params.id;
      this.dataLoaded = false;
      this.loadCategories();
    }
  }
}
</script>

<style>

</style>