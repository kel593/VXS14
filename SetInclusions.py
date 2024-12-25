# ***
# This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
# If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
# This Source Code Form is "Incompatible With Secondary Licenses", as defined by the Mozilla Public License, v. 2.0.
# ***

# Copies inclusions over to the game module
# Its set to completely wipe folders with matching names
# So if you add "Administration" as an inclusion - it will wipe the "Administration" folder with it

import os
import shutil

notice = """// This is an inclusion from the AntiCheat module.
// Changes here will be removed on the next build.
// To edit the anticheat code, use files from the Anticheat/Anticheat.Inclusions project."""

inclusions = "./Anticheat.Inclusions/"
dirmap = {
    "Server": "Content.Server",
    "Shared": "Content.Shared",
    "Client": "Content.Client"
}


def copy_inclusions_to_content(folder_name):
    source_path = os.path.join(inclusions, folder_name)
    destination_path = os.path.join("..", dirmap[folder_name])

    if not os.path.exists(source_path):
        return

    # Create the destination directory if it doesn't exist
    os.makedirs(destination_path, exist_ok=True)

    # Copy each subdirectory from source to destination
    for item in os.listdir(source_path):
        s = os.path.join(source_path, item)
        d = os.path.join(destination_path, item)
        if os.path.isdir(s):
            # Check if the subdirectory exists in the destination and remove it
            if os.path.exists(d):
                shutil.rmtree(d)
            shutil.copytree(s, d)

        mark_cs_files(d, notice)

    print(f"Copied {folder_name} successfully")


def mark_cs_files(folder_path, text):
    for root, _, files in os.walk(folder_path):
        for file in files:
            if file.endswith('.cs'):
                file_path = os.path.join(root, file)
                with open(file_path, 'r', encoding='utf-8') as f:
                    content = f.read().rstrip()
                with open(file_path, 'w', encoding='utf-8') as f:
                    f.write(text.strip() + '\n' + content)

def main():
    for dir in dirmap:
        copy_inclusions_to_content(dir)


if __name__ == "__main__":
    main()
