# 📖University-works
Здесь представлены некоторые университетские работы

# Оглавление
- [📐✏️Графы](#графы)
  - [Shortest path problem](#shortest-path-problem)
  - Eulerian path
  - Breadth-first search
- 🟨🔺Графический редактор фигур
  - Graphical shape editor
  - Graphic editor of figures. Grouping
 
# 📐✏️Графы 
Алгоритмы на графах на WF.

# Shortest path problem 
В данной программе реализована задача о кратчайшем пути в WF. ЛКМ на поле для создания вершины. Если нет выделенных вершин, ПКМ на вершину для её выделения. Если есть выделенная вершина, ПКМ на вершину для создания одностороннего пути с определенным весом. ЛКМ на вершину для выделения вершин, игнорируемых в поиске пути. Перед запуском алгорима необходимо выбрать конечную вершину в поле и выбрать начальную вершину ПКМ. Если номера конечной вершины нет в графе или до неё нет пути, то выходит соответствующее сообщение.

# Eulerian path
В данной программе реализован Эйлеров цикл в WF. ЛКМ на поле для создания вершины. Если нет выделенных вершин, ПКМ на вершину для её выделения. Если есть выделенная вершина, ПКМ на вершину для создания пути. Перед запуском алгоритма необходимо выбрать начальную вершину ПКМ. После запуска реализуется анимация алгоритма на графе. По очереди выделяются пути от вершины к вершине. Если хотя бы одна вершина не четная, то выходит сообщение о номере этой вершины.

# Breadth-first search
В данной программе реализован Поиск в ширину в WF. ЛКМ на поле для создания вершины. Если нет выделенных вершин, ПКМ на вершину для её выделения. Если есть выделенная вершина, ПКМ на вершину для создания пути. Перед запуском алгоритма необходимо выбрать начальную вершину ПКМ. После запуска реализуется анимация алгоритма на графе. Сначала помечаются все соседние вершины, а затем по очереди выделяются пути к ним, и т.д.

# 🟨🔺Графический редактор фигур
Графический редактор фигур на WF.

# Graphical shape editor
В данной программе реализован Простой графический редактор фигур в WF. ЛКМ для создания выбранной фигуры. ПКМ/ЛКМ для выделения, ЛКМ для удаления. Зажать ЛКМ на объекте для его перемещения.

Горячии клавиши:
 - Z - предыдущая фигура
 - X - следующая фигура
 - С - удалить фигуру
 - V - уменьшить размер
 - В - увеличить размер
 - N - увеличить количество углов
 - M - уменьшить количество углов
 - "<" ">" - вращать
 - W.A,S,D - перемещение
 - 1 - случайная звезда
 - 2 - случайный многоугольник
 - 3 - случайный круг

# Graphic editor of figures. Grouping
В данной программе реализован Простой графический редактор фигур в WF с возможностью группировки объектов, сохранения/загрузки и отмены последнего действия. ЛКМ для выделения фигур на поле, ПКМ - в списке. ЛКМ в списке для расгруппировки. 

Группировка объектов реализована с помощью паттерна Composite. Сохранение/загрузка реализованы с помощью  Abstract Factory и Factory Method
