#Ulesanne 1

N = int(input("Сколько чисел? "))
max_num = -float('inf')
for _ in range(N):
    num = int(input())
    if num > max_num:
        max_num = num
print("Максимальное:", max_num)N = int(input("Сколько чисел? "))
max_num = -float('inf')
for _ in range(N):
    num = int(input())
    if num > max_num:
        max_num = num
print("Максимальное:", max_num)


#Ulesanne 2


while True:
    num = int(input())
    if num == 0:
        break
    print(77 if num == 13 else num)


#Ulesanne 3


dist = 10
total = 0
for _ in range(7):
    total += dist
    dist *= 1.1
print("Сумма:", round(total, 2))


#Ulesanne 4

M = int(input("Длина ткани: "))
while M > 0:
    piece = int(input("Отрезок: "))
    if piece > M:
        if input("Купить остаток? (да/нет): ") == "да":
            print("Остаток:", M)
            break
    else:
        M -= piece
print("Ткань закончилась")


#Ulesanne 5

while True:
    a = float(input("a: "))
    b = float(input("b: "))
    h = float(input("h: "))
    print("Площадь:", (a + b) * h / 2)
    if input("Еще? (да/нет): ") != "да":
        break


#Ulesanne 6

num = int(input("Число: "))
print("Делится" if num % 3 == 0 else "Не делится")

