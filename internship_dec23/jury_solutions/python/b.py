n = int(input())
numbers = input().split()
s = sum([int(x) for x in numbers])

current_sum = 0
for i in range(n - 1):
    current_sum += int(numbers[i])
    if 2 * current_sum == s:
        print('+'.join(numbers[:i + 1]), '=', '+'.join(numbers[i + 1:]), sep='')
        exit()

print(-1)
