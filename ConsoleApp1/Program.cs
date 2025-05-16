// See https://aka.ms/new-console-template for more information
using ConsoleApp1.Classes;
using System.Runtime.ConstrainedExecution;

Console.WriteLine("Testing!!!");

//✅ 1.Binary Gap
//Contexto:
//Estás trabajando con números binarios y quieres encontrar la secuencia más larga de ceros consecutivos que esté rodeada por unos en ambos extremos.
//Ejemplo de entrada:
//N = 529 → binario: 1000010001
//Resultado esperado: 4(hay una secuencia de 4 ceros entre unos)

//✅ 2.Cyclic Rotation
//Contexto:
//Tienes un arreglo de enteros y necesitas rotarlo hacia la derecha K veces. Cada rotación mueve el último elemento al principio del arreglo.
//Ejemplo:
//A = [3, 8, 9, 7, 6], K = 3
//Resultado: [9, 7, 6, 3, 8]

//✅ 3.Odd Occurrences in Array
//Contexto:
//Se te da un arreglo en el que todos los elementos aparecen un número par de veces, excepto uno que aparece solo una vez. Encuentra ese número.
//Ejemplo:
//A = [9, 3, 9, 3, 9, 7, 9]
//Resultado: 7

//✅ 4.Tape Equilibrium
//Contexto:
//Tienes una cinta (arreglo de enteros) y quieres cortarla en dos partes. Debes encontrar el punto donde la diferencia absoluta entre las sumas de ambos lados sea mínima.
//Ejemplo:
//A = [3, 1, 2, 4, 3]
//Cortes posibles → mínima diferencia es 1

//✅ 5. Frog River One
//Contexto:
//Una rana quiere cruzar un río saltando sobre hojas que caen en posiciones del 1 al X. Dado un arreglo donde cada índice representa un segundo y el valor la posición donde cae una hoja, determina el primer segundo en que la rana puede cruzar el río (cuando todas las posiciones del 1 al X están cubiertas).
//Ejemplo:
//X = 5, A = [1, 3, 1, 4, 2, 3, 5, 4]
//Resultado: 6

//✅ 6.Missing Integer
//Contexto:
//Dado un arreglo de enteros, encuentra el número entero positivo más pequeño que no aparece en el arreglo.
//Ejemplo:
//A = [1, 3, 6, 4, 1, 2]
//Resultado: 5

//✅ 7.Max Product of Three
//Contexto:
//Se te da un arreglo de enteros. Encuentra el producto más grande posible de cualquier tres números (pueden ser negativos o positivos).
//Ejemplo:
//A = [-3, 1, 2, -2, 5, 6]
//Resultado: 60(producto de - 3 * -2 * 10)

//✅ 8.Number of Disc Intersections
//Contexto:
//Imagina que tienes varios discos en una línea (como círculos con centro en i y radio A[i]). Tu tarea es contar cuántos pares de discos se intersectan.
//Ejemplo:
//A = [1, 5, 2, 1, 4, 0]
//Resultado: 11(hay 11 pares de discos que se tocan o se superponen)
