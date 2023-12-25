n, k = map(int, input().split())
s = input()
ans = 0
for ch in 'RGB':
    cur, l = 0, 0
    for i in range(n):
        if ch != s[i]:
            cur += 1
        while l < i and cur > k:
            if ch != s[l]:
                cur -= 1
            l += 1
        ans = max(ans, i - l + 1)
print(ans)
