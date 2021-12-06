<template>
  <div class="container-fluid">
    <div class="row">
      <CategoryModal
        v-if="dialogIsVisible"
        @close="hideDialog"
        @onCreateCategory="onCreateCategory"
        >
        <h3 slot="heading">Create Category</h3>
        <button class="btn btn-warning" @click="hideDialog">Close</button>
      </CategoryModal>
    </div>
    <div class="row">
      <div class="col-12 mb-3">
        <button class="btn btn-outline-primary" @click="showDialog">Create Category</button>
      </div>
    </div>

    <div class="row">
      <div class="col-md-3">
        <div class="data-wrapper card p-3" :style="{ backgroundColor: 'coral' }">
          <h4>Total Categories</h4>
          <h5>{{ vmCategorySummery.totalCategory }}</h5>
        </div>
      </div>
      <div class="col-md-3">
        <div class="data-wrapper card p-3" :style="{ backgroundColor: 'azure' }">
          <h4>Categories have expense</h4>
          <h5>{{ vmCategorySummery.categoryHaveData }}</h5>
        </div>
      </div>
      <div class="col-md-3">
        <div class="data-wrapper card p-3" :style="{ backgroundColor: 'beige' }">
          <h4>Categories without expense</h4>
          <h5>{{ vmCategorySummery.categoryWithoutData }}</h5>
        </div>
      </div>
    </div>

    <hr>
    <div class="row">
      <div class="col-12">
        <h4>Select category to view details</h4>
      </div>
    </div>
    <div class="row">
      <div class="col-3">
        <Treeselect v-model="vmCategoryId" :options="vmCategoryTree"></Treeselect>
      </div>
    </div>
      
    <hr>

    <div class="row">
      <div class="col-md-12">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import Card from "@scripts/components/Cards/Card.vue";
import CategoryModal from "@scripts/components/CategoryModal.vue";
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import ApplicationContextService from '@scripts/Services/ApplicationContextService';
import CategoryService from '@scripts/Services/CategoryService';

export default {
  components: {
    Card,
    CategoryModal,
    Treeselect
  },
  data() {
    return {
      dialogIsVisible: false,
      vmCategoryId: 0,
      vmCategoryTree: [],
      vmCategorySummery: {
        totalCategory: 0,
        categoryHaveData: 0,
        categoryWithoutData: 0
      },
    };
  },
  mounted() {
    this.loadCategories();
  },
  methods: {
    loadCategories() {
      let companyId = ApplicationContextService.getContext().company.id;
      CategoryService.getAllCategories(companyId)
        .then(response => {
          this.vmCategoryTree = JsonTreeBuilder.buildTree(response);
          this.vmCategoryTree.unshift({ id: 0, label: 'All', children: [] });
          this.prepareCategorySummery(response);
        });
    },
    onCreateCategory(category) {
      CategoryService.createCategory(category).then(response => {
        this.loadCategories();
        this.vmCategoryId = response.data.parentId || response.data.id;
      });
    },
    showDialog() {
      this.dialogIsVisible = true;
    },
    hideDialog() {
			this.dialogIsVisible = false;
		},
    prepareCategorySummery(response) {
      this.vmCategorySummery.totalCategory = response.length;
      response.forEach(c => {
        if (c.expenses.length != 0)
          this.vmCategorySummery.categoryHaveData++;
      });
      this.vmCategorySummery.categoryWithoutData = this.vmCategorySummery.totalCategory - this.vmCategorySummery.categoryHaveData;
    },
  },
  watch: {
    vmCategoryId() {
      console.log('root-cat-id: ', this.vmCategoryId);
      this.$router.push({ params: { id: this.vmCategoryId } });
    }
  }
}
</script>

<style>
</style>