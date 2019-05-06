# https://www.codewars.com/kata/roboscript-number-1-implement-syntax-highlighting

import re
import codewars_test as Test


def highlight(code):
    ret = code
    ret = re.sub(r"(F+)", '<span style="color: pink">\\1</span>', ret)
    ret = re.sub(r"(L+)", '<span style="color: red">\\1</span>', ret)
    ret = re.sub(r"(R+)", '<span style="color: green">\\1</span>', ret)
    ret = re.sub(r"([0-9]+)", '<span style="color: orange">\\1</span>', ret)
    return ret


if __name__ == "__main__":
    Test.describe("Your Syntax Highlighter")
    Test.it("should work for the examples provided in the description")
    print("Code without syntax highlighting: F3RF5LF7")
    print("Your code with syntax highlighting: " + highlight("F3RF5LF7"))
    print(
        'Expected syntax highlighting: <span style="color: pink">F</span><span style="color: orange">3</span><span style="color: green">R</span><span style="color: pink">F</span><span style="color: orange">5</span><span style="color: red">L</span><span style="color: pink">F</span><span style="color: orange">7</span>'
    )
    Test.assert_equals(
        highlight("F3RF5LF7"),
        '<span style="color: pink">F</span><span style="color: orange">3</span><span style="color: green">R</span><span style="color: pink">F</span><span style="color: orange">5</span><span style="color: red">L</span><span style="color: pink">F</span><span style="color: orange">7</span>',
    )
    print("Code without syntax highlighting: FFFR345F2LL")
    print("Your code with syntax highlighting: " + highlight("FFFR345F2LL"))
    print(
        'Expected syntax highlighting: <span style="color: pink">FFF</span><span style="color: green">R</span><span style="color: orange">345</span><span style="color: pink">F</span><span style="color: orange">2</span><span style="color: red">LL</span>'
    )
    Test.assert_equals(
        highlight("FFFR345F2LL"),
        '<span style="color: pink">FFF</span><span style="color: green">R</span><span style="color: orange">345</span><span style="color: pink">F</span><span style="color: orange">2</span><span style="color: red">LL</span>',
    )
