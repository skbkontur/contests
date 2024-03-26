n, t = map(int, input().split())
if sum(map(int, input().split())) <= t <= sum(map(int, input().split())):
    print('YES')
else:
    print('NO')
