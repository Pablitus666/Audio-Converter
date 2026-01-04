🎉 AudioConverter v1.0.0 — Initial Stable Release

Primera versión estable de AudioConverter, una aplicación de escritorio para conversión de audio desarrollada en C# (.NET / WinForms), orientada a uso real, con arquitectura sólida, progreso real y cancelación segura.

Este release marca el cierre de la fase de desarrollo base y el inicio de una versión lista para producción.

✨ Características principales
🔁 Conversión por lotes (Batch)

Conversión simultánea de múltiples archivos de audio

Gestión independiente por job

Estados claros por archivo

📊 Progreso real

Progreso calculado a partir de datos reales de FFmpeg

Uso de -progress pipe:1

Barra de progreso precisa y confiable (no simulada)

⛔ Cancelación segura

Cancelación individual por archivo

Cancelación global del batch completo

Finalización forzada del proceso FFmpeg y su árbol (Kill(true))

Sin procesos huérfanos ni zombies

🎧 Formatos soportados

WAV

MP3

FLAC

🎚️ Opciones de audio

Frecuencia de muestreo configurable

Canales (Mono / Stereo)

Profundidad de bits

Formato de salida seleccionable

🖥️ Interfaz

UI clara y moderna (WinForms personalizado)

Tabla de jobs con:

Archivo

Progreso

Estado

Cancelación individual

Bloqueo inteligente de controles durante la conversión

📦 FFmpeg embebido

No requiere FFmpeg instalado

Ejecutable portable

Extracción automática del binario en tiempo de ejecución

🧱 Arquitectura

Separación clara entre UI, Core y Runner

Lógica de conversión desacoplada

ConversionJob independiente por archivo

CancellationTokenSource por job y global

Diseño escalable y mantenible

🛠️ Requisitos

Windows 10 / 11

.NET Desktop Runtime compatible con WinForms

No se requieren dependencias externas

🚀 Instalación

Descargar el archivo .zip desde este release

Extraer el contenido

Ejecutar AudioConverter.exe

No requiere instalación adicional.

📦 Estado del proyecto

✔️ Estable
✔️ Listo para uso real
✔️ Arquitectura preparada para futuras mejoras

🔮 Próximas mejoras planificadas

ETA / tiempo restante por archivo

Soporte para más formatos

Perfiles de calidad avanzados

Migración a WPF

Cola persistente entre sesiones

📄 Licencia

Este proyecto se distribuye bajo la licencia MIT.

👨‍💻 Autor

Pablo Téllez
Contacto: pharmakoz@gmail.com