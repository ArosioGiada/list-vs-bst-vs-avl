using System;
using System.Collections.Generic; // to use 'List<T>'
using System.Diagnostics; // to use 'Stopwatch'

namespace DataStructureComparison
{
    class Program
    {
        // Main method
        // Entry point of the program. It initializes the data structures, populates them with random numbers, and provides a menu for the user to search for a value in each data structure (List, BST, and AVL) and return results.
        // To do so:
        // 1. Created:
        //     _ List of int called 'numberList'
        //     _ BinarySearchTree called 'bst'
        //     _ AVLTree called 'avl'
        //     _ 'random' to generate random numbers

        // 2. To insert - Filled all three structures with 150,000 random integers (range: 0–199999). To do so:
        //     _ For loop - set 'i' to 0, with condition 'until i < 150000' , add 1 to 'i'. Until condition true:
        //         - Generate a random number using ''random.Next(200000)'' and store it in the created int called 'r'
        //         - Add the number 'r' to the 'numberList' using Add(r)
        //         - Insert the number into the BinarySearchTree (bst) calling Insert()
        //         - Insert the number into the AVL tree (avl) calling Insert()

        // 3. To search and print results - Open a while loop that will run until the user decide to exit the program. Here:
        //     a) Display user menu, printing the menu options and prompt user to enter their option
        //     b) Read user input, remove whitespace using .Trim() and store in a string called 'choice'
        //           Handle user choice using if/else if/else:
        //           _ If OPTION "1":
        //                 1. Prompt the user to enter a number
        //                 2. Read user input, remove whitespace using .Trim() and store in a string called 'input'
        //                 3. if/else : Validate the input using int.TryParse and store it in ''searchValue''
        //                    - If input valid, start to search and measure the time taken, to do so:

        //                              a) Inizialize and start Stopwatch, called 'sw', to measure the time
        //                              b) Search in List using Contains() and store the result in a bool called 'foundList'
        //                              c) Stop Stopwatch
        //                              d) Print the result (FOUND / NOT FOUND) and the time in milliseconds
        //                              -------
        //                              a) Restart Stopwatch
        //                              b) Search in BinarySearchTree calling Search() and store the result in a bool called 'foundBST'
        //                              c) Stop Stopwatch
        //                              d) Print the result (FOUND / NOT FOUND) and the time in milliseconds
        //                              -------
        //                              a) Restart Stopwatch
        //                              b) Search in AVLTree calling Search() and store the result in a bool called 'foundAVL'
        //                              c) Stop Stopwatch
        //                              d) Print the result (FOUND / NOT FOUND) and the time in milliseconds

        //                    - If input invalid: print an error message. The program go back to WHILE printing the menu
        //           _ If OPTION "2":
        //                 Print goodbye message and stop the program.
        //           _ OTHERWISE:
        //                 Print an error message asking a valid option (1 or 2). The program go back to WHILE printing the menu

        // NOTE: in this program the 3 structures has the same numbers so if a number is found in one, will be found in all of them, but the time taken by the process will be different do to the characteristics of each structure.
        static void Main(string[] args)
        {
            List<int> numberList = new List<int>();
            BinarySearchTree bst = new BinarySearchTree();
            AVLTree avl = new AVLTree();
            Random random = new Random();

            for (int i = 0; i < 150000; i++)
            {
                int r = random.Next(200000);

                numberList.Add(r);
                bst.Insert(r);
                avl.Insert(r);
            }

            while (true)
            {
                Console.WriteLine("\n----- MENU -----");
                Console.WriteLine("1. Search for a number in all data structures");
                Console.WriteLine("2. Exit");
                Console.Write("\nEnter option (1-2): ");

                string choice = Console.ReadLine().Trim();

                if (choice == "1")
                {
                    Console.Write("\nPlease enter a number to search: ");
                    string input = Console.ReadLine().Trim();

                    if (int.TryParse(input, out int searchValue))
                    {
                        Console.WriteLine($"\n---- Search results ----");

                        Stopwatch sw = Stopwatch.StartNew();
                        bool foundList = numberList.Contains(searchValue);
                        sw.Stop();
                        Console.WriteLine($"\n- List: {(foundList ? "FOUND" : "NOT FOUND")} in {sw.Elapsed.TotalMilliseconds} ms");

                        sw.Restart();
                        bool foundBST = bst.Search(searchValue);
                        sw.Stop();
                        Console.WriteLine($"\n- Binary Search Tree: {(foundBST ? "FOUND" : "NOT FOUND")} in {sw.Elapsed.TotalMilliseconds} ms");

                        sw.Restart();
                        bool foundAVL = avl.Search(searchValue);
                        sw.Stop();
                        Console.WriteLine($"\n- BALANCED Binary Search Tree(AVL): {(foundAVL ? "FOUND" : "NOT FOUND")} in {sw.Elapsed.TotalMilliseconds} ms\n");
                    }
                    else
                    {
                        Console.WriteLine("--- Invalid input. Please enter an integer. ---\n");
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n---- Goodbye ----\n");
                    break;
                }
                else
                {
                    Console.WriteLine("--- Invalid option. Please enter 1 or 2. ---\n");
                }
            }
        }
    }

    // Class ''Node''
    // This class is used by both 'BinarySearchTree' and 'AVLTree' to build and manage the structure of the tree.
    // Each node contains an integer value and references to its left and right children.
    // The class also includes 'Height' that tracks the height of the node within the tree. Used for balancing in AVL trees.
    // To do so:
    // 1. Declare:
    //     _ int called 'Value'
    //     _ node called 'Left': reference to the left child node
    //     _ node called 'Right': reference to the right child node
    //     _ int called 'Height'
    //
    // 2. Use a constructor that takes an int value as input and inizialize:
    //     _ 'Value' to store the value of the node (given value)
    //     _ Both 'Left' and 'Right' children to null (no children yet)
    //     _ 'Height' to 1.
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;
        public int Height;

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Height = 1;
        }
    }

    // Binary Search Tree class
    // Contains methods for insertion and searching of value in a tree structure. Do to so:
    // 1. Declares a public Node called 'Root'

    // 2. Public method 'Insert(int value)':
    //      Entry point from the Main method, to call the recursive method Insert(Node, int) and updates the tree starting from Root.
    // 3. Virtual (overrided by AVL Tree class) method 'Insert(Node node, int value)':
    //      Handles the recursive insertion logic. Navigates the tree to find the correct position
    //      for the new value and inserts it. Ignores duplicates. Designed to be overridden (e.g., in AVLTree).

    // 4. Public method 'Search(int value)':
    //      Entry point from the Main method, to call the recursive method Search(Node, int) and check if a value exists in the tree or not.
    // 5. Private method 'Search(Node node, int value)':
    //      Recursively go thought the tree and returns true if the value exists, false otherwise.
    public class BinarySearchTree
    {
        public Node Root;

        // Void method called 'Insert'. Use to call the Insertion of value received into the tree, stating from Root.
        // Is the entry point from the MAIN for the call of the recursive insertion mehtod. To do so:
        // Assign to Root the result of the recursive method, passing to it ''Root'' and the value given to insert. This could potentially return a new node if the tree was empty.
        // NOTE: Although the insertion logic for both BinarySearchTree and AVLTree could be used directly via Insert(Node, int) in the Main. I implemented this (inherited by AVLTree) to improves usability with a cleaner MAIN.
        public void Insert(int value)
        {
            Root = Insert(Root, value);
        }

        // Virtual method called 'Insert'. Recursively inserts the received value into the tree, starting from the given node.
        // This method is meant to be overridden by the AVLTree class.
        // 1. Recursively compares the value to insert, with each node along the path until it finds an empty spot where insert it. TO DO SO:
        //      _ if statement - check if the node given is null. If so, create and return a new Node with the received value stored. Otherwise:
        //      _ if/else if/else - compare the value to insert, with the value of the node:
        //          a) If smaller, recursively call Insert() on the left subtree and assigns the result to node.Left.
        //          b) If greater, recursively call Insert() on the right subtree and assigns the result to node.Right.
        //      _ return node.
        //      NOTE: If equal, nothing happen, ONLY return the node, because for efficency duplicates are ignored(pro compared to the list where also the duplicates are added and make the search even more slower).
        public virtual Node Insert(Node node, int value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            if (value < node.Value)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Insert(node.Right, value);
            }

            return node;
        }

        // Public method called 'Search'. Same as before with Insert(int value) and Insert(Node node, int value), I decide to create it to simplify usage of the method Search(Node node, int value) and make the MAIN cleaner.
        // It calls the recursive search, passing to it the root of the tree and the value received. And return the result of it(will be a boolean: true if number exist in the tree, false if not)
        public bool Search(int value)
        {
            return Search(Root, value);
        }

        // Private recursive method 'Search'. Navigates the all tree to check if the given value exists and return a boolean: true if number exist in the tree, false if not. Do to so:
        // 1. If statement - check if the node given is null. If so, value not found, return false. 

        // Otherwise:

        // 2. Compare the value with the node value to decide the direction:
        //     - If value is greater: recall Search() and keep search in the right subtree passing the node.right as node now, and the same value to search. And will return its result.
        //     - If value is smaller: recall Search() and keep search in the left subtree, passing the node.left as node now, and the same value to search. And will return its result.
        //     - If neither, mean is equal so the value is found, return true
        private bool Search(Node node, int value)
        {
            if (node == null)
            {
                return false;
            }

            if (value > node.Value)
            {
                return Search(node.Right, value);
            }
            else if (value < node.Value)
            {
                return Search(node.Left, value);
            }
            else
            {
                return true;
            }
        }
    }

    // AVL Tree class (inherits from BinarySearchTree)
    // It keepS the tree always balanced after every insertion, ensuring better performance, especially with large datasets.
    // It uses rotations to maintain a maximum height difference of 1 between left and right subtrees of each node.
    // 
    // 1. 'Insert' (override): Inserts values just like in the base class, but in addition updates the height and rebalances the tree when needed.
    //     - If a node becomes unbalanced after insertion, it detects the type of imbalance and apply correct rotation to restore balance.
    // 
    // 2. 'GetHeight': Returns the height of a node. Used to calculate balance factor and update height after rotations.
    // 
    // 3. 'GetBalance': Calculates the balance factor of a node (left height - right height). Used to decide if a rotation is needed.
    // 
    // 4. 'RotateLeft': Rebalances the tree when the right subtree is too tall.
    // 
    // 5. 'RotateRight': Rebalances the tree when the left subtree is too tall.
    public class AVLTree : BinarySearchTree
    {
        // Override method called 'Insert'. Inserts the value received into the tree starting from the given node like the Insert of BinarySearchTree but the difference is that it also ensures the tree remains balanced after insertion.
        // It is called recursively to find the correct position and update the balance.
        // 1. Insertion, recursively compares the value to insert, with each node along the path until it finds an empty spot where insert it. TO DO SO:
        //      _ if statement - check if the node given is null. If so, create and return a new Node with the received value stored. Otherwise:

        //      _ if/else if/else - compare the value to insert, with the value of the node:
        //          a) If smaller, recursively call Insert() on the left subtree and assigns the result to node.Left.
        //          b) If greater, recursively call Insert() on the right subtree and assigns the result to node.Right.
        //          c) If equal, ONLY return the node, because for efficency duplicates are ignored.

        // NOTE: This part is different from the normal SBT. Every time we insert, it also check and balance the tree if needed. TO DO SO:
        // 2. Update the height:
        //      _ Use 'GetHeight' to retrieve the height of left and right children of the node passed.
        //      _ Assign to 'node.Height' the maximum of the two heights +1.

        // 3. Calculate the balance factor:
        //      _ Call 'GetBalance' method and store the result in int 'balance' to determine if the node became unbalanced

        // 4. Check if current subtree is balanced or if a rotation is needed(balance factor outside range -1, 1). If needed, rotatate in based on balance factor and insertion direction. 4 cases:
        //      _ If balance > 1 and the new value is less than the value in the left child = Right Rotation
        //      _ If balance < -1 and the new value is greater than the value in the right child = Left Rotation
        //      _ If balance > 1 and the new value is greater than the value in the left child = Left Rotation on left child, then Right Rotation
        //      _ If balance < -1 and the new value is less than the value in the right child = Right Rotation on right child, then Left Rotation

        // 5. Return the (rotated or not) node.
        public override Node Insert(Node node, int value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            if (value < node.Value)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Insert(node.Right, value);
            }
            else
            {
                return node;
            }

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && value < node.Left.Value)
            {
                return RotateRight(node);
            }

            if (balance < -1 && value > node.Right.Value)
            {
                return RotateLeft(node);
            }

            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Method int called ''GetHeight''. Used to returns the height of the node recieved if not null. To do so:
        // 1. If statemnt to check if the node is null (meaning no subtree exists):
        //      - if so, returns 0
        // 2. Otherwise, return the height of the node.
        public int GetHeight(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }

        // Method int called 'GetBalance'. Returns the balance factor of the node received.
        // Used to determine if the portion of the tree starting from the given node is balanced or not after insertion. Do to so:
        // 1. If statement to check if the node is null:
        //     - if null, returns 0
        // 2. Otherwise, use the call of 'GetHeight' to subtract the height of the right child from the left one. Returns the height difference (left - right).    
        public int GetBalance(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        // Method called 'RotateLeft'. Make a left rotation on the tree that has the recieved node as root.
        // In order to balance the AVL tree when the right subtree result too long(not balanced) respect the left one. To do so:
        // 1. Create node 'newRoot' and store the right child of 'root'. This node will become the new root.
        // 2. Create node 'temp' to store the left child of 'newRoot'

        // 3. Perform the rotation:
        //      - Set the left child of 'newRoot' to the current 'root' (move 'root' under 'newRoot')
        //      - Set the right child of 'root' to 'temp' (reattach original left child of 'newRoot' as right child of 'root')

        // 4. Update heights of the rotated nodes:
        //      - Update height of 'root' using GetHeight on its children
        //      - Update height of 'newRoot' (now new subtree root)

        // 5. Return 'newRoot' as the new root node after the rotation.
        private Node RotateLeft(Node root)
        {
            Node newRoot = root.Right;
            Node temp = newRoot.Left;

            newRoot.Left = root;
            root.Right = temp;

            root.Height = Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1;
            newRoot.Height = Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;

            return newRoot;
        }

        // Method 'RotateRight'. Make a right rotation on the tree that has the recieved node as root.
        // In order to balance the AVL tree when the left subtree result too long(not balanced)respect the right one. To do so:
        // 1. Create node 'newRoot' and store the left child of 'root'. This node will become the new root.
        // 2. Create node 'temp' to store the right child of 'newRoot'

        // 3. Perform the rotation:
        //      - Set the right child of 'newRoot' to the current 'root' (move 'root' under 'newRoot')
        //      - Set the left child of 'root' to 'temp' (reattach original right child of 'newRoot' as left child of 'root')

        // 4. Update heights of the rotated nodes:
        //      - Update height of 'root' using GetHeight on its children
        //      - Update height of 'newRoot' (now new subtree root)

        // 5. Return 'newRoot' as the new root node after the rotation.
        private Node RotateRight(Node root)
        {
            Node newRoot = root.Left;
            Node temp = newRoot.Right;

            newRoot.Right = root;
            root.Left = temp;

            root.Height = Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1;
            newRoot.Height = Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;

            return newRoot;
        }
    }
}