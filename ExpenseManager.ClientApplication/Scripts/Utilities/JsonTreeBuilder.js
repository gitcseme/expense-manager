export default {
  buildTree(nodes, id = null) {
    let map = {};
    for (let i = 0; i < nodes.length; ++i) 
      map[nodes[i].id] = i;

    for (let i = 0; i < nodes.length; ++i) 
      if (nodes[i].parentId != null)
        nodes[map[nodes[i].parentId]].children.push(nodes[i]);
    
    let roots = [];
    if (id == null || id == 0) roots = nodes.filter(node => node.parentId == null);
    else roots = nodes.filter(node => node.id == id);
    return roots;
  }
};
