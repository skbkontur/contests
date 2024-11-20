n = int(input())
throwsX = 0
throwsY = 0
for i in range(n):
    x, y = map(int, input().split())
    throwsX -= x
    throwsY -= y
for i in range(n):
    x, y = map(int, input().split())
    throwsX += x
    throwsY += y
print(throwsX // n, throwsY // n)
