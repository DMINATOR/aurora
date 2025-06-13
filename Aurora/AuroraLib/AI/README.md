# AI Module

Module for performing AI operations

## Install

First the ollama needs to be downloaded;

Available models:

| **Model**               | **Size**  | **Context Length** | **Precision** | **Notes** |
|-------------------------|----------|--------------------|--------------|-----------|
| phi4-mini:latest       | 2.5GB    | 128K               | Standard FP  | General-purpose version |
| phi4-mini:3.8b         | 2.5GB    | 128K               | Standard FP  | Same as `latest`, explicitly labeled with parameter count |
| phi4-mini:3.8b-q4_K_M  | 2.5GB    | 128K               | **Quantized (Q4)** | Optimized for lower memory usage |
| phi4-mini:3.8b-q8_0    | 4.1GB    | **4K**             | **Quantized (Q8)** | Higher precision but lower context length |
| phi4-mini:3.8b-fp16    | **7.7GB** | **4K**             | **Full FP16** | Highest precision, largest memory footprint |

- <https://github.com/ollama/ollama/blob/main/docs/windows.md#standalone-cli>
- Release `ollama-windows-amd64.zip` from <https://github.com/ollama/ollama/releases/tag/v0.6.8>
- Download and unpack to a folder contents

## Running via endpoint api

- [See API for complete reference.](https://github.com/ollama/ollama/blob/main/docs/api.md)

### Get list of local models:

- [Reference - get tags](https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models)

Request:

```curl
curl http://localhost:11434/api/tags
```

Response:

```json
{
  "models": [
    {
      "name": "phi4-mini:latest",
      "model": "phi4-mini:latest",
      "modified_at": "2025-05-15T22:53:48.1099388+03:00",
      "size": 2491876774,
      "digest": "78fad5d182a7c33065e153a5f8ba210754207ba9d91973f57dffa7f487363753",
      "details": {
        "parent_model": "",
        "format": "gguf",
        "family": "phi3",
        "families": [
          "phi3"
        ],
        "parameter_size": "3.8B",
        "quantization_level": "Q4_K_M"
      }
    }
  ]
}
```

- In this case the phi4-model is installed.


## Running via CLI

`ollama.exe` can be used as a standalone CLI

- First start the server at the install path, run `ollama.exe serve`
- From another shell, run `ollama run phi4-mini` to install and run [phi-4 model](https://techcommunity.microsoft.com/blog/educatordeveloperblog/welcome-to-the-new-phi-4-models---microsoft-phi-4-mini--phi-4-multimodal/4386037)

Once model is running, try to run some prompts directly, see <https://github.com/microsoft/PhiCookBook/tree/main/md/02.Application/04.Vision/Phi4/CreateFrontend>

Example prompt for Phi-4

```
>>> <|user|>Can you let me know what's your favorite color?<|end|><|assistant|>
As an AI, I don't have personal preferences or feelings like humans do. However, the concept of a "favorite" can be fascinating from many different perspectives
if we think about popular choices among people worldwide.

Many studies and surveys suggest that blue is often considered one of people's most favored colors due to its calming properties in various cultures around the
globe. Blue evokes emotions such as serenity or tranquility for some individuals.


Would you like me to look into more specific data on favorite color preferences? Or perhaps there's a particular aspect you're interested in exploring regarding
this topic!
```

- To exit, type `/bye` in the prompt and `ctrl-c` to stop the server.

## Troubleshooting

- Installation path - `explorer %HOMEPATH%\.ollama`

- [Troubleshooting guide](https://www.llamafactory.cn/ollama-docs/en/troubleshooting.html)