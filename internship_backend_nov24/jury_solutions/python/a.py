n = int(input())
res = ''
mx = -1
for i in range(n):
    s = input()
    unique = len(set(s))
    if unique > mx:
        mx = unique
        res = s
print(mx, res, sep = ' ')
