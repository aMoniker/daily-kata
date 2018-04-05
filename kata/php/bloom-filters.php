<?php

/**
 * Bloom Filters
 *
 * http://codekata.com/kata/kata05-bloom-filters
 */

class BloomFilter
{
    private $map = [];
    private $hashAlgo = 'md5';
    private $hashChunks = 6;

    public function __construct($set = [])
    {
        foreach ($set as $element) {
            $this->add($element);
        }
    }

    /**
     * Add element to the filter
     *
     * @param string $element
     */
    public function add($element)
    {
        foreach ($this->generateHashKeys($element) as $key) {
            $this->map[$key] = true;
        }
    }

    /**
     * Check if an element is possibly in the bloom filter,
     * or definitely not in the filter.
     *
     * @param  string $element
     * @return bool
     */
    public function check($element)
    {
        foreach ($this->generateHashKeys($element) as $key) {
            if (empty($this->map[$key])) {
                return false;
            }
        }
        return true;
    }

    /**
     * Generate N hash decimal values where N = $this->hashChunks
     *
     * @param  string $element
     * @return array
     */
    private function generateHashKeys($element)
    {
        $hash = hash($this->hashAlgo, $element);
        $chunks = str_split($hash, $this->hashChunks);
        array_pop($chunks);

        return array_map(function($chunk) {
            return hexdec($chunk);
        }, $chunks);
    }
}


$filter = new BloomFilter();

$handle = fopen('/usr/share/dict/words', 'r');
while (($word = fgets($handle)) !== false) {
    $filter->add(substr($word, 0, -1));
}
if (!feof($handle)) {
    echo "Error: unexpected fgets() fail\n";
}
fclose($handle);

var_dump('animal?', $filter->check('animal'));
var_dump('coffee?', $filter->check('coffee'));
var_dump('book?', $filter->check('book'));
var_dump('fhwfhewhfs?', $filter->check('fhwfhewhfs'));
var_dump('fdafdaf?', $filter->check('fdafdaf'));
