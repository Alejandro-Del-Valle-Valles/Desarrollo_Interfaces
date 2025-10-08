# Trivial Geográfico de Europa
---
> Alejandro del Valle Vallés

**Ejercicio Práctico de Desarrollo de Interfaces**

*Enunciado*:

Hay que hacer una aplicación con los siguientes requisitos:

- Tendrá 2 páginas, países y capitales, además de la principal. En la principal habrá dos botones para seleccionar una de las otras páginas.

- En la página países se mostrará aleatoriamente un país y 4 ciudades, una de ellas debe ser la capital del país mostrado, el usuario deberá seleccionar una de las ciudades. Si la respuesta es la capital correcta se sumara a un acumulador de aciertos, si no es correcta a un acumulador de fallos. Se mostrará un botón para pasar al siguiente país con sus 4 ciudades, como antes una de ellas la capital, con la misma lógica descrita, hasta que se responda a la capital de 10 países, indicándose a continuación el numero de aciertos y fallos.

- Para página de capitales la misma dinámica, excepto que se mostrará una ciudad capital de un país, y 4 países entre los que estará la respuesta correcta, hasta responder a 10 preguntas.

- Para cada una de las paginas, no se debe volver a preguntar la misma capital ni el mismo país una vez hayan sido seleccionados como pregunta entre las 10 preguntas.

- La lista de países y capitales estará almacenada en dos colecciones (arrays) ocupando cada capital y su país la misma posición para facilitar el acceso a los valores: