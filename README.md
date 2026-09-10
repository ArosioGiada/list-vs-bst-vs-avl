# List vs BST vs AVL

A performance comparison of three data structures — a `List<int>`, a custom Binary Search Tree, and a custom AVL Tree — searching for values among 150,000 randomly inserted integers.

## Overview

The program builds all three structures with the same 150,000 random integers (range 0–199,999), then presents a menu letting you search for a number in all three at once. Each search is timed independently with `Stopwatch`, so you can directly compare how long a linear search, a BST search, and a balanced AVL search take on identical data.

    ----- MENU -----
    1. Search for a number in all data structures
    2. Exit
    Enter option (1-2):

## Structures

- **`List<int>`**: the .NET Standard Library's dynamic array. `Contains()` does a linear scan — O(n).
- **`BinarySearchTree`**: a custom BST built from scratch, with recursive insert and search. No self-balancing, so its performance depends on the shape the tree happens to take.
- **`AVLTree`**: inherits from `BinarySearchTree` and overrides `Insert` to rebalance the tree with rotations after every insertion, guaranteeing O(log n) height regardless of insertion order.

## Sample results (150,000 integers)

| Search | List | BST | AVL |
|---|---|---|---|
| Value not present (`3679`) | 1.2052 ms | 0.9584 ms | 0.0318 ms |
| Value present (`40`) | 0.1290 ms | 0.0423 ms | 0.0480 ms |

The List is consistently the slowest, as expected from a linear O(n) scan. The AVL Tree wins clearly on the "not found" search, where the full guaranteed O(log n) height advantage shows up — a search that fails still has to reach the bottom of the tree, and the AVL's stricter balancing keeps that bottom much closer.

## Why build an AVL if the BST looks similar here?

The numbers are inserted in **random order**, and a BST built from random insertions tends to end up close to balanced by chance — so in this test, the plain BST often performs close to the AVL. That's expected, not a flaw in the comparison: the real gap between the two only shows up with **non-random insertion order** (e.g. inserting already-sorted data), where a plain BST degrades toward a linked list (O(n) height), while the AVL Tree's rotations keep it at O(log n) no matter what order the data arrives in. The AVL's guarantee is about worst-case behaviour, not just this particular random dataset.

## How to run

    dotnet run

Choose option 1, enter a number to search for, and the timings for all three structures are printed. Choose option 2 to exit.

> Project built for the Algorithms and Data Structures unit at Torrens University Australia.
