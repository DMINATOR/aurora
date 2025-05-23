﻿# AI Module

Module for performing AI operations

## Install

First the ollama needs to be downloaded;

- <https://github.com/ollama/ollama/blob/main/docs/windows.md#standalone-cli>
- Release `ollama-windows-amd64.zip` from <https://github.com/ollama/ollama/releases/tag/v0.6.8>
- Download and unpack to a folder contents

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
