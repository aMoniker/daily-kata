/**
 * Today's kata is quicksort using
 * the Lomuto partition scheme
 * It runs 10k times with an array of 1k elements
 * and checks that the elements were sorted.
 */

for (let i = 0; i < 10000; i++) {
    let foo = Array.from({length: 1000}, () =>
        Math.floor(Math.random() * 1000)
    );
    quicksort(foo, 0, foo.length - 1);
    check(foo);
}

function check(a) {
    let highest = a[0];
    for (let i = 1; i < a.length; i++) {
        if (highest > a[i]) {
            throw Error('not in order!');
        }
    }
};

// algorithm quicksort(A, lo, hi) is
//     if lo < hi then
//         p := partition(A, lo, hi)
//         quicksort(A, lo, p - 1 )
//         quicksort(A, p + 1, hi)
function quicksort(a, lo, hi) {
    if (lo < hi) {
        let p = partition(a, lo, hi);
        quicksort(a, lo, p - 1);
        quicksort(a, p + 1, hi);
    }
};

// algorithm partition(A, lo, hi) is
//     pivot := A[hi]
//     i := lo - 1
//     for j := lo to hi - 1 do
//         if A[j] < pivot then
//             i := i + 1
//             swap A[i] with A[j]
//     swap A[i + 1] with A[hi]
//     return i + 1
function partition(a, lo, hi) {
    let pivot = a[hi];
    let i = lo - 1;
    for (let j = lo; j < hi; j++) {
        if (a[j] < pivot) {
            i = i + 1;
            swap(a, i, j);
        }
    }
    swap(a, i + 1, hi);
    return i + 1;
};

function swap(a, i, j) {
    let temp = a[i];
    a[i] = a[j];
    a[j] = temp;
}
