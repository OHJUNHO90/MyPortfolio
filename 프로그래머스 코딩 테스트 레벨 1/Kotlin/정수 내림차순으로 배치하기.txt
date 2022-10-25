class Solution {
    fun solution(n: Long): Long = String(n.toString().toCharArray().sortedArrayDescending()).toLong()
}