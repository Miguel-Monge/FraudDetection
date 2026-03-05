# Fraud Detection - Pagatelia

Sistema de detección de fraude que identifica órdenes fraudulentas cuando un mismo usuario intenta comprar el mismo deal múltiples veces usando distintas tarjetas de crédito.

## Requisitos

- .NET 10.0

La aplicación lee desde el console input y escribe el resultado en la salida estándar.

## Formato de entrada

- **Línea 1:** Entero N (número de registros)
- **Líneas posteriores:** Un registro por línea con campos separados por coma:
  - Order id, Deal id, Email, Street, City, State, Zip, Credit Card

## Reglas de fraude

Una orden es fraudulenta si cumple cualquiera de estas condiciones:

1. **Mismo email + deal, distinta tarjeta:** Dos órdenes comparten email y deal id pero usan tarjetas diferentes.
2. **Misma dirección + deal, distinta tarjeta:** Dos órdenes comparten Address/City/State/Zip y deal id pero usan tarjetas diferentes.

## Normalización (evasión de detección)

El sistema aplica normalización para detectar intentos de evasión:

| Campo | Reglas |
|-------|--------|
| **Email** | Case insensitive. El punto (.) se ignora en la parte local. El `+` y todo lo que sigue se ignora (ej. `bugs+10@bunny.com` = `bugs@bunny.com`). |
| **Dirección** | Case insensitive. Street/St., Road/Rd. se consideran equivalentes. IL=Illinois, CA=California, NY=New York. |
