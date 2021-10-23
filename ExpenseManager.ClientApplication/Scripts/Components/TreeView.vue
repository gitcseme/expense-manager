<template>
	<div class="tree-wrapper">
		<div v-html="vmTree"></div>
	</div>
</template>

<script>
export default {
	props: ['tree', 'marginLeft'],
	data() {
		return {
			vmTree: null,
			node: '<button class="btn btn-info mt-1" style="margin-left: @px">$</button>',
		};
	},
	mounted() {
		this.loadTree();
	},
	methods: {
		loadTree() {
			let htmlTree = this.buildHtmlTree(this.tree, ``, 0);
			this.vmTree = htmlTree;
		},

		buildHtmlTree(tree, html, margin) {
			tree.forEach(item => {
				if (item.parentId !== null) {
					html = html + `<span style="margin-left: ${margin}px">↓________</span>`;
				}
				html = html + this.node.replace('$', item.label).replace('@', 0) + '<br/>';
				if (item.children.length !== 0) {
					let inner_html = this.buildHtmlTree(item.children, ``, margin + this.marginLeft) + ``;
					html = html + inner_html;
				}
			});
			return `${html}`;
		}
	}
}
</script>

<style>

</style>