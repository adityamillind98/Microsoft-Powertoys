// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "ClassFactory.h"

HINSTANCE   g_hInst     = NULL;
long        g_cDllRef   = 0;

// {C0075768-9543-4730-8384-3CD506D59AD8}
static const GUID CLSID_DevFilesPreviewHandler = { 0xc0075768, 0x9543, 0x4730, { 0x83, 0x84, 0x3c, 0xd5, 0x6, 0xd5, 0x9a, 0xd8 } };

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        g_hInst = hModule;
        DisableThreadLibraryCalls(hModule);
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

//
//   FUNCTION: DllGetClassObject
//
//   PURPOSE: Create the class factory and query to the specific interface.
//
//   PARAMETERS:
//   * rclsid - The CLSID that will associate the correct data and code.
//   * riid - A reference to the identifier of the interface that the caller
//     is to use to communicate with the class object.
//   * ppv - The address of a pointer variable that receives the interface
//     pointer requested in riid. Upon successful return, *ppv contains the
//     requested interface pointer. If an error occurs, the interface pointer
//     is NULL.
//
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{
    HRESULT hr = CLASS_E_CLASSNOTAVAILABLE;

    if (IsEqualCLSID(CLSID_DevFilesPreviewHandler, rclsid))
    {
        hr = E_OUTOFMEMORY;

        ClassFactory* pClassFactory = new ClassFactory();
        if (pClassFactory)
        {
            hr = pClassFactory->QueryInterface(riid, ppv);
            pClassFactory->Release();
        }
    }

    return hr;
}

//
//   FUNCTION: DllCanUnloadNow
//
//   PURPOSE: Check if we can unload the component from the memory.
//
//   NOTE: The component can be unloaded from the memory when its reference
//   count is zero (i.e. nobody is still using the component).
//
STDAPI DllCanUnloadNow(void)
{
    return g_cDllRef > 0 ? S_FALSE : S_OK;
}
