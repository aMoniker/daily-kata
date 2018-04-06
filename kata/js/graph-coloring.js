/**
 * Graph Coloring
 *
 * Given an undirected graph with maximum degree D,
 * find a graph coloring using at most D+1 colors.
 *
 * https://www.interviewcake.com/question/javascript/graph-coloring
 */

class Graph {
  constructor(maxDegree) {
    this.nodes = new Set();
    this.nodeLabelStart = 1;
    this.maxDegree = maxDegree;
  }

  makeNode() {
    let degree = 0;
    let node = new GraphNode(this.nodeLabelStart);
    this.nodeLabelStart++;
    for (let n of this.nodes) {
      if (n.neighbors.size < this.maxDegree) {
        n.neighbors.add(node);
        node.neighbors.add(n);
      }
      if (node.neighbors.size >= this.maxDegree) {
        break;
      }
    }
    this.nodes.add(node);
  }

  makeNodes(count = 1) {
    for (let i = 0; i < count; i++) {
      this.makeNode();
    }
  }

  fillColors() {
    this.nodes.forEach((node) => {
      let usedColors = new Set();
      node.neighbors.forEach((n) => {
        if (n.color) {
          usedColors.add(n.color);
        }
      });

      let i = 0;
      while (++i) {
        if (!usedColors.has(i)) {
          node.color = i;
          break;
        }
      }
    });
  }

  getDegree() {
    let highestDegree = 0;

    for (let node of this.nodes) {
      if (node.neighbors.size > highestDegree) {
        highestDegree = node.neighbors.size;
      }
    }

    return highestDegree;
  }

  getNumColors() {
    let color = 0;
    for (let node of this.nodes) {
      if (node.color > color) { color = node.color; }
    }
    return color;
  }
}

class GraphNode {
  constructor(label) {
    this.label = label;
    this.neighbors = new Set();
    this.color = null;
  }
}

let graph = new Graph(10);
graph.makeNodes(100);
graph.fillColors();

// test
let degree = graph.getDegree();
let colors = graph.getNumColors();
if (colors > degree + 1) {
  throw `Expected no more than ${degree + 1} colors, but got ${colors}!`;
}
console.log('Tests pass!');
