#include <bits/stdc++.h>

int main() {
    int size, border, changes;
    std::cin >> size >> border >> changes;

    std::vector<int> elements;
    
    for (int i = 0; i < size; i++) {
        int x;
        std::cin >> x;
        elements.push_back(x);
    }
    
    int left = 0;
    int right = 0;
    int currentChanges = 0;
    int maxLength = 0;
    while (right < size) {
        if (elements[right] < border) {
            right++;
            continue;
        }

        if (currentChanges < changes) {
            currentChanges++;
            right++;
            continue;
        }

        maxLength = std::max(maxLength, right - left);

        while (elements[left] < border) 
            left++;
    
    
        left++;
        right++;
    }

    maxLength = std::max(maxLength, right - left);

    std::cout << maxLength;
}