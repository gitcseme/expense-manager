<template>
  <div class="modal-wrapper">
    <div class="backdrop" @click="$emit('close')"></div>
    <dialog open>
      <div class="row">
        <div class="col-12">
          <slot name="heading"></slot>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div class="card p-3">
            <div class="form-group">
              <label>Category Name</label>
              <input type="text" class="form-control area-width" v-model="vmCategory.title" />
            </div>
            <div class="parent-category" v-if="!vmCreateAsParent">
              <p>Select parent</p>
              <Treeselect v-model="vmCategory.parentId" :options="vmCategoryTree"></Treeselect>
            </div>
            <div class="form-group form-check">
              <input type="checkbox" class="form-check-input" v-model="vmCreateAsParent">
              <label class="form-check-label">Create as parent</label>
            </div>
          </div>
        </div>
      </div>
      <slot></slot>
      <button class="btn btn-success ml-1" :disabled='shouldDisable' @click="createCategory">Create</button>
      <!-- <button v-if="mode === 'create'" class="btn btn-success" @click="CreateXategory">Save</button>
      <button v-else class="btn btn-success" @click="updateIncome">Update</button> -->
    </dialog>
  </div>
</template>

<script>
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import CategoryAPI from "@scripts/API/CategoryAPI";
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";

export default {
  components: {
    Treeselect
  },
  //props: ['category', 'mode'],
	data() {
		return {
			vmCategory: { 
        title: '', 
        parentId: null,
        companyId: ApplicationContextService.getContext().company.id
      },
      vmCreateAsParent: true,
      vmCategoryTree: []
		};
	},
  mounted() {
    this.loadCategoryTree();
  },
  computed: {
    shouldDisable() {
      if (this.vmCategory.title == '' || (!this.vmCreateAsParent && this.vmCategory.parentId == null))
        return true;

      return false;
    }
  },
	methods: {
    createCategory() {
      if (this.vmCategory.parentId === undefined || this.vmCreateAsParent)
        this.vmCategory.parentId = null;
        
      this.$emit('onCreateCategory', this.vmCategory);
      this.$emit('close');
    },
    loadCategoryTree() {
      let companyId = ApplicationContextService.getContext().company.id
      console.log('cat-modal-companyId: ', companyId);
      CategoryAPI.getAllCategories(companyId)
      .then(data => {
        this.vmCategoryTree = JsonTreeBuilder.buildTree(data);
        console.log('built tree:) ', this.vmCategoryTree);
      });
    },
  }
}
</script>

<style>
/* .backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  z-index: 10;
  background-color: rgba(0, 0, 0, 0.75);
}

dialog {
  position: fixed;
  top: 15vh;
  width: 33rem;
  left: calc(50% - 15rem);
  margin: 0;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.26);
  border-radius: 3px;
  padding: 1rem;
  background-color: cadetblue;
  z-index: 100;
  border: none;
} */
</style>