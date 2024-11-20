def isInBounds(x, y, n, m):
    return x >= 0 and x < n and y >= 0 and y < m

def dfs(startX, startY, setNumber, n, m, setsNumbers, field, used, steps):
    stack = []
    stack.append((startX, startY))
    used[startX][startY] = setNumber

    while(len(stack) != 0):
        (currentX, currentY) = stack[-1]
        stack.pop()
        setsNumbers[setNumber]+=1
        for (stepX, stepY) in steps:
            posX = currentX+stepX
            posY = currentY+stepY
            if isInBounds(posX, posY, n, m) and field[posX][posY] == 'X' and used[posX][posY] == 0:
                stack.append((posX, posY))
                used[posX][posY] = setNumber
                
    

steps = [ (1, 0), (-1, 0), (0, 1), (0, -1) ]

n,m,q = map(int, input().split())
setsNumbers = [0 for i in range(n * m)]
field = [ [0 for i in range(m)] for i in range(n)]
used = [ [0 for i in range(m)] for i in range(n)]

for i in range(n):
    line = input()
    for j in range(m):
        field[i][j] = line[j]

setCounter = 0

for i in range(n):
    for j in range(m):
        if used[i][j] == 0 and field[i][j] == 'X':
            setCounter+=1
            dfs(i, j, setCounter, n, m, setsNumbers, field, used, steps)

for i in range(q):
    x, y = map(int, input().split())
    x-=1
    y-=1
    if used[x][y] == 0:
        print("MISS")
    else:
        setsNumbers[used[x][y]]-=1
        if setsNumbers[used[x][y]] == 0:
            print("DESTROY")
        else:
            print("HIT")
