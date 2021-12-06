<template>
    <div class="row pagination-section">
        <div class="col-3">
            View
            <select class="form-page-select" v-model="vmPagination.pageSize" @change="onPageSizeChange">
                <option v-for="page in vmPageSizes" :key="page" :value="page">
                    {{page}}
                </option>
            </select>
            {{ itemLabel }} per page
        </div>
        <div class="col-6 pagination justify-content-center">
            <ul v-if="vmPagination.itemsTotal > 0" class="pagination-pages">
                <li class="previous-btn" v-if="vmPagination.pageIndex > 1">
                    <a class="page-link" @click="onPageNumberClicked(vmPagination.pageIndex-1)">
                        <i class="ti-angle-left"></i>{{ vmPreviousPageText }}
                    </a>
                </li>
                <li v-for="page in vmPagination.pages" :key="page" :class="{ 'active-page': page == vmPagination.pageIndex }">
					<a class="page-link" v-if="page == vmPagination.pageIndex">{{ page }}</a>
                    <a class="page-link" v-else @click="onPageNumberClicked(page)">{{ page }}</a>
                </li>
                <li class="next-btn" v-if="vmPagination.pageIndex < vmPagination.pagesTotal">
                    <a class="page-link" @click="onPageNumberClicked(vmPagination.pageIndex+1)">
                        {{ vmNextPageText }}<i class="ti-angle-right"></i>
                    </a>
                </li>
            </ul>
        </div>
        <div class="col-3" style="text-align: right">
            Showing {{ vmPagination.itemsFrom }} to {{ vmPagination.itemsTo }} of {{ vmPagination.itemsTotal }} {{ itemLabel }}
        </div>
    </div>
</template>

<script>

export default {
    props: {
        label: {
            type: String,
            default: 'items'
        },
        value: Object,
    },
    components: {
        
    },
    data() {
        return {
            vmPreviousPageText: 'Previous',
            vmNextPageText: 'Next',
            vmPageSizes: [10, 20, 30, 40, 50],
            itemLabel: this.label,
            vmPagination: this.value,
        };
    },
    mounted() {
        
    },
    methods: {
        onPageNumberClicked(pageIndex) {
			this.vmPagination.pageIndex = pageIndex;
			this.$emit('pageChanged', pageIndex);
		},
		onPageSizeChange() {
			this.$emit('pageSizeChanged', this.vmPagination.pageSize);
		}
	},
	watch: {
		value: function(newValue) {
			this.vmPagination = newValue;
		}
	}
};
</script>