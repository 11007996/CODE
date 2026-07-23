import numpy as np

arr1 = np.array([True, False, True], dtype=bool)
arr2 = np.array([False, True, False], dtype=bool)

result_and = np.bitwise_and(arr1, arr2)   # 与
result_or = np.bitwise_or(arr1, arr2)     # 或
result_xor = np.bitwise_xor(arr1, arr2)    # 异或   对应位上的两个数字相异时，结果为ture 1；相同时，结果为false 0。
result_not = np.bitwise_not(arr1)       # 非

print("AND:", result_and)  # [False, False, False]
print("OR:", result_or)    # [True, True, True]
print("XOR:", result_xor)  # [True, True, True]
print("NOT:", result_not)  # [False, True, False]

# 按位取反
arr_invert = np.invert(np.array([1, 2], dtype=np.int8))
print("Invert:", arr_invert)  # [-2, -3]    # 取反

# 左移位运算
arr_left_shift = np.left_shift(5, 2)    #   左移
print("Left Shift:", arr_left_shift)  # 20

# 右移位运算
arr_right_shift = np.right_shift(10, 1)   # 右移
print("Right Shift:", arr_right_shift)  # 5