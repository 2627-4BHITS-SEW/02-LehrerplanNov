using UnityEngine;
using UnityEngine.Networking;

public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // Return true to blindly accept the certificate.
        // WARNING: This disables security checks and should only be used for local development/testing.
        return true;
    }
}