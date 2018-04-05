/**
 * Find the 2nd largest element in a binary search tree.
 *
 * https://www.interviewcake.com/question/javascript/second-largest-item-in-bst
 */

class BinaryTreeNode {
  constructor(value) {
    this.value = value;
    this.left  = null;
    this.right = null;
  }
}

/**
 * recursively map an ordered array of values
 * into a tree structure of BinaryTreeNode objects
 */
const mapTree = (values) => {
  let pivot = Math.ceil((values.length - 1) / 2);
  let node = new BinaryTreeNode(values[pivot]);

  let leftSlice = values.slice(0, pivot);
  let rightSlice = values.slice(pivot + 1);

  node.left = leftSlice.length ? mapTree(leftSlice) : null;
  node.right = rightSlice.length ? mapTree(rightSlice) : null;

  return node;
};

/**
 * 1) Iterate along the right side of the tree to find the
 * largest (rightmost) value while keeping track of the last traversed node.
 *
 * 2) If the rightmost node is a leaf (no right or left nodes),
 * then the previous node (its parent) must be the second highest.
 *
 * 3) Otherwise, if the rightmost node has a left node, then descend to that.
 *
 * 4) Traverse any right nodes (if any), until there are no more.
 *    That one must be the second highest.
 */
const findSecondHighestNode = (node) => {
  // step 1)
  let curNode = node;
  let lastNode = node;
  while (curNode.right) {
    lastNode = curNode;
    curNode = curNode.right;
  }

  // step 2)
  if (curNode.left === null) {
    return lastNode;
  }

  // step 3)
  curNode = curNode.left;

  // step 4)
  while (curNode.right) {
    curNode = curNode.right;
  }
  return curNode;
};

// successively test the values by slicing off the
// last value end and building a new tree out of each slice
let values = Array(2000).fill().map((_, i) => i);
while (values.length) {
  let secondHighest = findSecondHighestNode(mapTree(values));
  let expectedValue = values[Math.max(values.length - 2, 0)];
  if (expectedValue !== secondHighest.value) {
    throw `Expected ${expectedValue} but got ${secondHighest.value}`;
  }
  values = values.slice(0, values.length - 1);
}
console.log('Tests passed!');
