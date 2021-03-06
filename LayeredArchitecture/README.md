# Layered Architecture


## Task 1 (week 1)

Create BLL (business logic layer) and DAL (data-access layer) for Carting Service. Any implementation of the layered architecture can be used. Layers should be logically separated (via separate folders/namespaces).

### Functional Requirements:

1. Single entity – Cart
2. Cart has a unique id which is maintained (generated) on the client-side.
3. The following actions should be supported:
    - Get list of items of the cart object.
    - Add item to cart.
    - Remove item from the cart.
4. Each item contains the following info:
    - Id – required, id of the item in external system (see Task 2), int.
    - Name – required, plain text.
    - Image – optional, URL and alt text.
    - Price – required, money.
    - Quantity – quantity of items in the cart.

### Non-functional Requirements (NFR):

- Testability
- Extensibility

### Constraints
- NoSQL database for persistence layer (for example - https://www.litedb.org/).
- Layers should be logically separated.

## Task 2(week 2)

Create BLL (business logic layer) and DAL (data-access layer) for Catalog Service. You must follow Clean Architecture with physical layers separation (via separate DLLs).

### Constraints:

- SQL database for persistence layer (for example - Microsoft SQL Server Database File).
- Layers should be physically separated.
 
### Non-functional Requirements (NFR):

- Testability
- Extensibility

### Functional Requirements:

1. Key entities: Category, Item (aka Product).
2. Category has:
    - Name – required, plain text, max length = 50.
    - Image – optional, URL.
    - Parent Category – optional
3. The following operations are allowed for Category: get/list/add/update/delete.
4. Item has:
    - Name – required, plain text, max length = 50.
    - Description – optional, can contain html.
    - Image – optional, URL.
    - Category – required, one item can belong to only one category.
    - Price – required, money.
    - Amount – required, positive int.
5. The following operations are allowed for Item: get/list/add/update/delete.
