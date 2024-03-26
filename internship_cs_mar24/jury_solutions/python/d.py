pattern = ''.join(chr(ch) + chr(ch).upper() for ch in range(ord('a'), ord('z') + 1))

sz = len(pattern)
pos = dict((pattern[i], i) for i in range(sz))

s = input()
n = len(s)
t = [pos[s[i]] for i in range(n)]
f = [0] * (n + 1)
q = int(input())
for _ in range(q):
    l, r, x = map(int, input().split())
    f[l - 1] += x
    f[r] -= x
for i in range(1, n):
    f[i] += f[i - 1]
print(''.join(pattern[(t[i] + f[i]) % sz] for i in range(n)))
