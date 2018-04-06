var recoverSecret = function(triplets) {
  for(var [first] of triplets)
  {
    if (triplets.every(tuple => tuple.indexOf(first) <= 0))
    {
      triplets.filter(([item]) => {
        return item == first;
      }).forEach(tuple => tuple.shift());
      return first + recoverSecret(triplets.filter(tuple => {
        return tuple.length > 0
      }));
    }
  }
  return '';
};

// Example
console.log(recoverSecret([
  [ 't', 's', 'f' ],
  [ 'a', 's', 'u' ],
  [ 'm', 'a', 'f' ],
  [ 'a', 'i', 'n' ],
  [ 's', 'u', 'n' ],
  [ 'm', 'f', 'u' ],
  [ 'a', 't', 'h' ],
  [ 't', 'h', 'i' ],
  [ 'h', 'i', 'f' ],
  [ 'm', 'h', 'f' ],
  [ 'a', 'u', 'n' ],
  [ 'm', 'a', 't' ],
  [ 'f', 'u', 'n' ],
  [ 'h', 's', 'n' ],
  [ 'a', 'i', 's' ],
  [ 'm', 's', 'n' ],
  [ 'm', 's', 'u' ]
]));
