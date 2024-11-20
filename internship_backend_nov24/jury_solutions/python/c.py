size, border, changes = map(int, input().split())
elements = list(map(int, input().split()))
left = 0
right = 0
currentChanges = 0
maxLength = 0
while (right < size):
    if elements[right] < border:
        right += 1
        continue
    if currentChanges < changes:
        currentChanges += 1
        right += 1
        continue
    maxLength = max(maxLength, right - left)

    while elements[left] < border:
        left += 1

    left += 1
    right += 1
maxLength = max(maxLength, right - left)
print(maxLength)
