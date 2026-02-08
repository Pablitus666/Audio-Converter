# 🎵 AudioConverter

AudioConverter es una aplicación de escritorio desarrollada en **C# (.NET / WinForms)** que permite convertir archivos de audio de forma **rápida, segura y profesional**, utilizando **FFmpeg embebido** y una arquitectura moderna orientada a jobs.

El proyecto está diseñado como una herramienta **robusta para uso real**, con soporte de **batch conversion**, **progreso real**, **cancelación por archivo** y **cancelación global**, manteniendo una experiencia de usuario clara y fluida.

---
![Platform](https://img.shields.io/badge/platform-Windows-0078D6?style=flat&logo=windows&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![Language](https://img.shields.io/badge/language-C%23-239120?style=flat&logo=csharp&logoColor=white)
![UI](https://img.shields.io/badge/UI-WinForms-0B5ED7?style=flat)
![FFmpeg](https://img.shields.io/badge/Powered%20by-FFmpeg-black?style=flat&logo=ffmpeg)
![Status](https://img.shields.io/badge/status-stable-brightgreen?style=flat)
![License](https://img.shields.io/badge/license-MIT-green?style=flat)

---

![Social Preview](images/Preview.png)

---

## ✨ Características principales

* 🔁 **Conversión por lotes (Batch)** de múltiples archivos
* 📊 **Progreso real por archivo** usando `-progress pipe:1` de FFmpeg
* ⛔ **Cancelación individual y global** sin procesos zombies
* 🎚️ Configuración de:

  * Frecuencia de muestreo
  * Canales (Mono / Stereo)
  * Profundidad de bits
  * Formato de salida (WAV, MP3, FLAC)
* 📂 **Selección de carpeta de salida:** Guarda los archivos convertidos en la misma ubicación que el original por defecto, o elige una carpeta personalizada.
* 🎧 Formatos soportados:

  * WAV
  * MP3
  * FLAC
* 🧠 Arquitectura desacoplada (UI / Core / Runner)
* 🖥️ Interfaz moderna y clara (WinForms personalizado)
* ✨ **Soporte High-DPI:** Escalado nítido en pantallas de alta resolución para una experiencia visual óptima.
* 🚨 **Manejo de Errores Robustos:** Genera un 'crashlog.txt' para depuración y muestra mensajes amigables en caso de errores críticos.
* 📦 FFmpeg **embebido** (no requiere instalación externa)

---
📥 Formatos de entrada soportados 

El convertidor admite una amplia variedad de formatos de audio y **video (para extracción de audio)** como entrada, gracias al uso del motor FFmpeg. A continuación, se presenta una lista representativa de los formatos más comunes compatibles.

🎧 Formatos de audio (entrada)

MP3 — MPEG-1 Audio Layer III

AAC / M4A — Advanced Audio Coding (frecuente en contenedores .m4a)

FLAC — Free Lossless Audio Codec

WAV — Waveform Audio File Format

OGG / Vorbis — Contenedor OGG con códec Vorbis

WMA — Windows Media Audio

AIFF — Audio Interchange File Format

ALAC / M4A — Apple Lossless Audio Codec

Opus — Códec de audio moderno, eficiente y de alta calidad

🎬 Formatos de video (extracción de audio)

Estos formatos permiten extraer y convertir la pista de audio contenida en archivos de video:

MP4 — MPEG-4 Part 14 (audio AAC, ALAC, MP3, etc.)

MKV (Matroska) — Contenedor flexible con múltiples códecs

AVI — Audio Video Interleave

MOV — QuickTime File Format

WMV — Windows Media Video

FLV — Flash Video

WEBM — WebM (audio Vorbis u Opus)

ℹ️ Nota técnica

La compatibilidad real puede variar según el códec específico utilizado dentro del archivo.
En general, cualquier formato reconocido por FFmpeg debería ser procesable por la aplicación.

---

## 🖼️ Interfaz

La aplicación cuenta con una interfaz limpia y profesional:

* Título destacado
* Controles de conversión claramente separados
* Tabla con:

  * Archivo
  * Progreso
  * Estado
  * Botón Cancelar por archivo
* Bloqueo inteligente de UI durante conversiones

---

## 🧱 Arquitectura del proyecto

```
AudioConverter
│
├── Core
│   ├── Converter.cs        # Lógica de batch, estados y jobs
│   └── FFmpegRunner.cs     # Ejecución de FFmpeg, progreso y cancelación
│
├── Models
│   ├── ConversionJob.cs
│   ├── ConversionOptions.cs
│   └── ConversionStatus.cs
│
├── Helpers
│   └── Logger.cs
│
├── UI (WinForms)
│   ├── Form1.cs
│   ├── DataGridViewProgressColumn.cs
│   └── CustomMessageBoxForm.cs
│
└── FFmpeg
    └── ffmpeg.exe (embebido como recurso)
```

---
---

## 📷 Capturas de pantalla

<p align="center">
  <img src="images/screenshot.png?v=2" alt="Vista previa de la aplicación" width="600"/>
</p>

---

## 🔄 Flujo de conversión

1. El usuario añade archivos de audio
2. Se crean `ConversionJob` independientes
3. Cada job obtiene su duración real
4. FFmpeg se ejecuta con `-progress pipe:1`
5. El progreso se calcula en tiempo real
6. El usuario puede cancelar:

   * Un archivo individual
   * Todo el batch

---

## ⛔ Sistema de cancelación

La cancelación está implementada de forma **segura y correcta**:

* `CancellationTokenSource` por job
* Token global para batch
* Kill del proceso FFmpeg y su árbol (`process.Kill(true)`)
* Limpieza de recursos
* Estados claros (`Cancelled`, `Completed`, `Failed`)

No quedan procesos en segundo plano.

---

## 📊 Progreso real

El progreso **NO es simulado**.

Se calcula a partir de:

* Duración total real del archivo
* Tiempo procesado reportado por FFmpeg (`out_time`)
* Conversión a porcentaje exacto

Esto garantiza una barra de progreso **precisa y confiable**.

---

## 🛠️ Requisitos

* Windows 10 / 11
* .NET Desktop Runtime (compatible con WinForms)
* No requiere FFmpeg instalado

---

## 🚀 Ejecución

### Opción 1: Ejecutable

Descarga el ejecutable desde la sección **Releases** del repositorio: https://github.com/Pablitus666/Audio-Converter/releases

1. Descarga el `.zip`
2. Extrae el contenido
3. Ejecuta `AudioConverter.exe`

### Opción 2: Compilación manual

```bash
git clone https://github.com/Pablitus666/Audio-Converter.git
```

Abrir la solución `AudioConverter.sln` en Visual Studio y compilar.

---

## 📦 Estado del proyecto

✔️ Estable
✔️ Listo para uso real
✔️ Arquitectura escalable

---

## 🔮 Posibles mejoras futuras

* ETA / tiempo restante por archivo
* Soporte para más formatos
* Perfil de calidad avanzado
* Migración a WPF
* Cola persistente

---

## 📄 Licencia

Este proyecto se distribuye bajo la licencia **MIT**.

---

## 🤝 Contribuciones

Las contribuciones, sugerencias y mejoras son bienvenidas.  
Si encuentras un problema o tienes una idea, no dudes en abrir un *issue* o *pull request*.

---

## 👨‍💻 Autor

Proyecto creado con enfoque en **calidad, estabilidad y buenas prácticas**.

*   **Nombre:** Pablo Téllez
*   **Contacto:** pharmakoz@gmail.com

---

⚖️ Nota legal sobre FFmpeg

---

Este software utiliza FFmpeg, un proyecto de software de código abierto desarrollado por el FFmpeg Project, para la conversión y procesamiento de archivos de audio.

FFmpeg es un proyecto independiente y no está afiliado ni respaldado oficialmente por el autor de esta aplicación.
Todos los derechos sobre FFmpeg pertenecen a sus respectivos autores.

FFmpeg se distribuye bajo los términos de la licencia LGPL 2.1 (o GPL, según la configuración y compilación del binario utilizado).
El binario de FFmpeg incluido se utiliza únicamente como motor de conversión y no modifica su código fuente.

Para más información sobre FFmpeg y sus licencias, visite el sitio oficial:
https://ffmpeg.org

El usuario final es responsable de cumplir con las leyes locales aplicables y los términos de licencia correspondientes al uso de este software.